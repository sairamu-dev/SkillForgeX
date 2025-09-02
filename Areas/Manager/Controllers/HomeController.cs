using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using DevTaskFlow.Areas.Manager.ViewModels;
using DevTaskFlow.Repository_pattern.Repository;
using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Repository_pattern.Service.Services;
using AutoMapper;
using System.Text;
using System.Linq;

namespace DevTaskFlow.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Admin,Manager,Guest")]
    public class HomeController : Controller
    {
        private readonly PortalRoleService _portalRoleService;
        private readonly UserService _userService;
        private readonly AppDbContext _context;
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;

        private const string DevsNotAvailable = "Currently, no developers are available. The task will be automatically assigned once someone is free.";
        private const string DevsTaskAssigned = "The current task added for ";
        private const string NoSkillsFromDB = "Api is Disabled and no matching skills found in Database";
        private const string ApiError = "Error occrued in api skill extraction - No response received.";
        private const string InValidPrompt = "please provide a valid task description.";


        public HomeController(PortalRoleService portalRoleService, UserService userService, AppDbContext context, ApiService apiService, IMapper mapper)
        {
            _portalRoleService = portalRoleService;
            _userService = userService;
            _context = context;
            _apiService = apiService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {         
            List<UsersViewModel> userList = new List<UsersViewModel>();

            var users = _userService.GetDevUsers();
            userList = _mapper.Map<List<UsersViewModel>>(users);
            ViewBag.CurrentPage = "Manager";
            ViewBag.CurrentTab = "ViewTask";

            if (users == null) 
                return View(userList);

            return View(userList);
        }

        [HttpGet]
        public IActionResult Task()
        {
            CreateTaskViewmodel task = new CreateTaskViewmodel();
            task.Skills = getSkills();
            task.Projects = getProjects();
            task.PriorityList = getPriorityList();
            ViewBag.CurrentPage = "Manager";
            ViewBag.CurrentTab = "CreateTask";

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Task(CreateTaskViewmodel task)
        {
            task.Projects = getProjects();
            task.PriorityList = getPriorityList();
            ViewBag.CurrentPage = "Manager";
            ViewBag.CurrentTab = "CreateTask";

            #region validations

            if (!ModelState.IsValid)
            {
                var Errors = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .SelectMany(x => x.Value.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                ViewBag.ValidationErrorMessage = Errors;
                return View(task);
            }

            #endregion

            List<KeywordWithSkills> keywordWithSkills = new List<KeywordWithSkills>();
            HashSet<string> uniqueSkills = new HashSet<string>();
            var Api = _portalRoleService.GetApiDetail();

            #region skill extraction from Api or DB

            if (Api.IsActive)
                uniqueSkills = await getSkillsFromAPI(task.Description); // only process when api enables
            else 
                uniqueSkills = getSkillsFromDB(task.Description.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            #endregion

            if (uniqueSkills.Count == 0)
            {
                return View(task);
            }

            task.RequiredSkills = string.Join(",", uniqueSkills);

            if(GetUserRole() == "Admin")
                task.CreatedBy = (int)ViewModels.PortalRoles.Admin;
            else if (GetUserRole() == "Manager")
                task.CreatedBy = (int)ViewModels.PortalRoles.Manager;
            else
                task.CreatedBy = (int)ViewModels.PortalRoles.Guest;

            task.EstimatedDays = (task.EndDate - DateTime.Today).Days;
            Dictionary<int,int> usersWithRequirdSkillset = assigningTaskBasedSkillAndPriority(uniqueSkills, task.Priority);

            #region no users avilable for the current task

            if (usersWithRequirdSkillset.Count == 0)
            {
                task.Status = "In-Queue";
                _portalRoleService.AddTaskForDevUsers(_mapper.Map<Tasks>(task));
                ViewBag.ErrorMessage = DevsNotAvailable;
                return View(task);
            }

            #endregion

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                #region adding task for an user

                // sorting based on low Concurrent task
                usersWithRequirdSkillset = usersWithRequirdSkillset.OrderBy(x => x.Value)
                                            .ToDictionary(x => x.Key, x => x.Value);

                List<UsersViewModel> users = _mapper.Map<List<UsersViewModel>>(_userService.GetDevUsers());

                // then choosing low task developer
                var taskAssignedUser = users
                                .Where(x => x.ID == usersWithRequirdSkillset.First().Key).First();

                taskAssignedUser.ConcurrentTask = taskAssignedUser.ConcurrentTask ?? 0;
                taskAssignedUser.ConcurrentTask += 1;
                taskAssignedUser.ModifiedBy = (int)ViewModels.PortalRoles.Manager;
                _userService.UpdateTaskForDevUser(_mapper.Map<User>(taskAssignedUser));

                #endregion

                #region adding task for task table

                task.AssignedTo = taskAssignedUser.ID;
                _portalRoleService.AddTaskForDevUsers(_mapper.Map<Tasks>(task));
                ViewBag.SuccessMessage = DevsTaskAssigned + taskAssignedUser.UserName;

                #endregion

                _context.SaveChanges();
                transaction.Commit();
            }
            catch(Exception eq)
            {
                transaction.Rollback();
                ViewBag.ErrorMessage = $"error occured while processing your request - {eq.Message}";
            }

            return View(task);
        }

        [HttpGet]
        public IActionResult GetDeveloperTask(int Dev)
        {
            List<TasksViewModel> taskList = _mapper.Map<List<TasksViewModel>>(_portalRoleService.GetDeveloperTask(Dev));
            string DevName = _userService.GetDevUsers().Where(x => x.ID == Dev).Select(x => x.UserName).FirstOrDefault();
            ViewBag.CurrentPage = "Manager";

            foreach (TasksViewModel task in taskList)
            {
                task.ProjectName = _portalRoleService.GetProjects().Where(x => x.ID == task.ProjectID).Select(x => x.ProjectName).FirstOrDefault();
            }

            var projects = _portalRoleService.GetProjects();
            double completionPercentage = 100 / taskList.Count;

            var chartData = taskList.Select(task => new
            {
                Label = $"{task.ProjectName} - {task.Title}",
                Percentage = completionPercentage,
                ID = task.TaskID
            }).ToList();


            ViewBag.DEV = DevName;
            ViewBag.ChartData = chartData;
            return View(taskList);
            
            
        }

        #region Fetching skills & projects & priority

        /// <summary>
        /// get set of skills
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> getSkills()
        {
            var skills = _portalRoleService.GetSkillset();
            return skills.Select(x => new SelectListItem
            {
                Value = x.SkillSet.ToString(),
                Text = x.SkillSet
            }).ToList();
        }

        /// <summary>
        /// get set of project
        /// </summary>
        /// <returns></returns>

        private List<SelectListItem> getProjects()
        {
            var projects = _portalRoleService.GetProjects();
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "-- Select Project --" }
            }
            .Concat(projects.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.ProjectName
            })).ToList();
        }

        /// <summary>
        /// get priority list
        /// </summary>
        /// <returns></returns>

        private List<SelectListItem> getPriorityList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = "Low" },
                new SelectListItem { Text = "Medium", Value = "Medium" },
                new SelectListItem { Text = "High", Value = "High" }
            };
        }

        #endregion


        /// <summary>
        /// findout the developers with matching skills & task priority
        /// </summary>
        /// <param name="RequiredSkills"></param>
        /// <param name="TaskPriority"></param>
        /// <returns></returns>
        private Dictionary<int,int> assigningTaskBasedSkillAndPriority(HashSet<string> RequiredSkills, string TaskPriority)
        {
            List<UsersViewModel> users = _mapper.Map<List<UsersViewModel>>(_userService.GetDevUsers());
            Dictionary<int, int> usersWithRequirdSkillset = new Dictionary<int, int>();

            #region matchingSkills

            foreach (var user in users)
            {
                if ((user.ConcurrentTask ?? 0) <= 4) // concurrent working task maximum is 5
                {
                    string[] skillset = user.Skills.Split(",");
                    var matchedSkills = RequiredSkills
                                        .Select(s => s.Trim())
                                        .Intersect(skillset.Select(s => s.Trim()), StringComparer.OrdinalIgnoreCase);


                    if (RequiredSkills.Count == matchedSkills.Count()) // all values matched
                    {
                        usersWithRequirdSkillset.Add(user.ID, user.ConcurrentTask ?? 0);
                    }
                }
            }

            #endregion

            #region highPriority

            if (usersWithRequirdSkillset.Count == 0 && TaskPriority == "High")
            {
                foreach (var user in users)
                {
                    string[] skillset = user.Skills.Split(",");
                    var matchedSkills = RequiredSkills
                                        .Select(s => s.Trim())
                                        .Intersect(skillset.Select(s => s.Trim()), StringComparer.OrdinalIgnoreCase);

                    if (RequiredSkills.Count == matchedSkills.Count()) // all values matched
                    {
                        usersWithRequirdSkillset.Add(user.ID, user.ConcurrentTask ?? 0);
                    }
                }

                // sorting based on low Concurrent task
                usersWithRequirdSkillset = usersWithRequirdSkillset.OrderBy(x => x.Value)
                                            .ToDictionary(x => x.Key, x => x.Value);

                foreach (var DevInfo in usersWithRequirdSkillset)
                {
                    List<CreateTaskViewmodel> taskList = _mapper.Map<List<CreateTaskViewmodel>>(_portalRoleService.GetDeveloperTask(DevInfo.Key));
                    CreateTaskViewmodel taskView = new CreateTaskViewmodel();

                    if (taskList.Any(task => task.Priority == "Low"))
                    {
                        taskView = taskList.FirstOrDefault(x => x.Status == "Low");
                        _portalRoleService.MarkLowPriorityTaskAsInQueue(taskView.TaskID ?? 0);
                        usersWithRequirdSkillset[DevInfo.Key]--;
                        break;

                    }

                    else if (taskList.Any(task => task.Priority == "Medium"))
                    {
                        taskView = taskList.FirstOrDefault(x => x.Priority == "Medium");
                        _portalRoleService.MarkLowPriorityTaskAsInQueue(taskView.TaskID ?? 0);
                        usersWithRequirdSkillset[DevInfo.Key]--;

                        var user = _userService.GetDevUserByID(taskView.AssignedTo ?? 0);
                        user.ConcurrentTask--;
                        _userService.UpdateTaskForDevUser(user);
                        break;
                    }

                }

                return usersWithRequirdSkillset;
            }
            else
                return usersWithRequirdSkillset;

            #endregion

        }

        private string GetUserRole()
        {
            string role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            return role;
        }

        private void validateUser()
        {
            if (GetUserRole() == "Manager" || GetUserRole() == "Guest")
                ViewBag.CurrentPage = "Manager";
            else
                ViewBag.CurrentPage = "Admin";
        }

        private HashSet<string> getSkillsFromDB(string[] listOfKeyword)
        {
            List<KeywordWithSkills> keywordWithSkills = _portalRoleService.GetKeywordWithSkills();
            HashSet<string> uniqueSkills = new HashSet<string>();

            foreach (string keyword in listOfKeyword)
            {
                if (keywordWithSkills.Any(k => k.keyword.Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                {
                    string matchedSkills = keywordWithSkills
                                            .Where(x => string.Equals(x.keyword, keyword, StringComparison.OrdinalIgnoreCase))
                                            .Select(x => x.skills)
                                            .FirstOrDefault();


                    if (matchedSkills != null)
                    {
                        var splitMatchedValues = matchedSkills.Split(",");

                        foreach (string value in splitMatchedValues)
                        {
                            uniqueSkills.Add(value);
                        }
                    }
                }
            }

            if(uniqueSkills.Count() == 0)
                ViewBag.ErrorMessage = NoSkillsFromDB;

            return uniqueSkills;
        }

        private async Task<HashSet<string>> getSkillsFromAPI(string description)
        {
            HashSet<string> uniqueSkills = new HashSet<string>();
            var response = await _apiService.GenerateResponseAsync(description);

            if(response == "No response received." || response.Contains("ErrorResponse:")) 
                ViewBag.ErrorMessage = ApiError;
            else if(response.Trim().Equals("please provide a valid task description", StringComparison.OrdinalIgnoreCase) || response.Trim().Equals("please provide a valid task description.", StringComparison.OrdinalIgnoreCase))
                ViewBag.ErrorMessage = InValidPrompt;
            else
            {
                #region extracted keywords stored in DB

                List<KeywordWithSkills> keywordWithSkillsDB = _portalRoleService.GetKeywordWithSkills();
                var valuePairs = response.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                List<KeywordWithSkills> keywordWithSkills = new List<KeywordWithSkills>();
                List<string> requiredSkills = _portalRoleService.GetSkillset().Select(x => x.SkillSet).ToList();
                List<string> verifiedKeywords = new List<string>();


                foreach (var pair in valuePairs)
                {
                    var splitParts = pair.Split('-', 2);
                    if (splitParts.Length == 2)
                    {
                        string[] listOfSkills = splitParts[1].Split(",");

                        foreach(string skills in listOfSkills)
                        {
                            if(requiredSkills.Any(x => x.Equals(skills.Trim(), StringComparison.OrdinalIgnoreCase)))
                            {
                                verifiedKeywords.Add(skills.Trim());
                            }
                        }

                        keywordWithSkills.Add(new KeywordWithSkills { keyword = splitParts[0].Trim(), skills = string.Join(",", verifiedKeywords) });
                    }
                }

                foreach (var key in keywordWithSkills)
                {
                    if (!keywordWithSkillsDB.Any(x => x.keyword.Equals(key.keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        _portalRoleService.InsertKeywordWithSkills(key);
                    }

                    var splitValues = key.skills.Split(',');
                    foreach (var splitValue in splitValues)
                    {
                        uniqueSkills.Add(splitValue.Trim());
                    }
                }

                #endregion
            }

            return uniqueSkills;
        }

    }
}
