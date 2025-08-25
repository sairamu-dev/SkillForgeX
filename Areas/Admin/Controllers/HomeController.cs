using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Repository_pattern.Repository;
using DevTaskFlow.Repository_pattern.Service.Services;
using DevTaskFlow.Areas.Admin.ViewModels;
using DevTaskFlow.Areas.Manager.ViewModels;
using AutoMapper;
using ClosedXML.Excel;
using Tasks = DevTaskFlow.Repository_pattern.Core.Enitities.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using DevTaskFlow.Repository_pattern.Core.Interfaces;

namespace DevTaskFlow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly PortalRoleService _portalRoleService;
        private readonly UserService _userService;
        private readonly ErrorService _errorService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(PortalRoleService portalRoleService,UserService userService, ErrorService errorService, IMapper mapper, AppDbContext context)
        {
            _portalRoleService = portalRoleService;
            _userService = userService;
            _errorService = errorService;
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Tasks()
        {
            List<TaskViewModel> tasks = _mapper.Map<List<TaskViewModel>>(_portalRoleService.GetTasks());
            ViewBag.CurrentPage = "Admin";
            ViewBag.CurrentTab = "OverrideTask";
            return View(tasks);
        }

        [HttpGet]
        public IActionResult UpdateTask(int taskId)
        {
            EditTaskViewModel tasks = _mapper.Map<EditTaskViewModel>(_portalRoleService.GetTasks().Where(x => x.TaskID == taskId).FirstOrDefault());
            tasks.RequiredSkillsList = tasks.RequiredSkills.Split(',').ToList();
            tasks.Skills = getSkills();
            tasks.Projects = getProjects();
            tasks.PriorityList = getPriorityList();
            tasks.StatusList = getStatusList();
            tasks.availableDevUsers = GetusersList();
            ViewBag.CurrentPage = "Admin";
            ViewBag.CurrentTab = "OverrideTask";
            return View(tasks);
        }

        [HttpPost]
        public IActionResult UpdateTask(EditTaskViewModel taskViewModel)
        {
            taskViewModel.Skills = getSkills();
            taskViewModel.Projects = getProjects();
            taskViewModel.PriorityList = getPriorityList();
            taskViewModel.StatusList = getStatusList();
            taskViewModel.availableDevUsers = GetusersList();
            taskViewModel.RequiredSkills = string.Join(",", taskViewModel.RequiredSkillsList);
            ViewBag.CurrentPage = "Admin";
            ViewBag.CurrentTab = "OverrideTask";

            string[] skills = (taskViewModel.RequiredSkills).Split(',');

            if (!ModelState.IsValid)
            {
                var Errors = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .SelectMany(x => x.Value.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                ViewBag.ValidationErrorMessage = Errors;
                return View(taskViewModel);
            }

            var prevTask = _portalRoleService.GetTasks().Where(x => x.TaskID == taskViewModel.TaskID).FirstOrDefault();

            if (taskViewModel.Status == "In-Queue" || taskViewModel.Status == "Completed")
            {
                _portalRoleService.OverrideTask(_mapper.Map<Tasks>(taskViewModel));
                UsersViewModel users = _mapper.Map<UsersViewModel>(_userService.GetDevUsers().Where(x => x.ID == prevTask.AssignedTo).FirstOrDefault());
                users.ConcurrentTask = users.ConcurrentTask ?? 0;
                users.ConcurrentTask -= 1;
                users.ModifiedBy = (int)Manager.ViewModels.PortalRoles.Admin;
                _userService.UpdateTaskForDevUser(_mapper.Map<User>(users));

                _context.SaveChanges();
                ViewBag.SuccessMessage = taskViewModel.Status == "In-Queue" ? "Task has been put on hold" : "Task has been completed";
            }
            else if(prevTask.AssignedTo != taskViewModel.AssignedTo)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    UsersViewModel currentUsers = _mapper.Map<UsersViewModel>(_userService.GetDevUsers().Where(x => x.ID == taskViewModel.AssignedTo).FirstOrDefault());
                    currentUsers.ConcurrentTask = currentUsers.ConcurrentTask ?? 0;

                    if (currentUsers.ConcurrentTask < 5)
                    {
                        #region remove task from existing dev

                        UsersViewModel prevUsers = _mapper.Map<UsersViewModel>(_userService.GetDevUsers().Where(x => x.ID == prevTask.AssignedTo).FirstOrDefault());
                        prevUsers.ConcurrentTask = prevUsers.ConcurrentTask ?? 0;
                        prevUsers.ConcurrentTask -= 1;
                        prevUsers.ModifiedBy = (int)Manager.ViewModels.PortalRoles.Admin;
                        _userService.UpdateTaskForDevUser(_mapper.Map<User>(prevUsers));

                        #endregion

                        #region add task for new dev

                        currentUsers.ConcurrentTask += 1;
                        currentUsers.ModifiedBy = (int)Manager.ViewModels.PortalRoles.Admin;
                        _userService.UpdateTaskForDevUser(_mapper.Map<User>(currentUsers));

                        _portalRoleService.OverrideTask(_mapper.Map<Tasks>(taskViewModel));

                        _context.SaveChanges();
                        transaction.Commit();

                        ViewBag.SuccessMessage = $"The current task added for {currentUsers.UserName}";

                        #endregion
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Unable to assign the task to {currentUsers.UserName} as they have reached their task limit.";
                        return View(taskViewModel);
                    }
                }
                catch (Exception eq)
                {
                    transaction.Rollback();
                    ViewBag.ErrorMessage = $"error occured while processing your request - {eq.Message}";
                }
            }
            else if(prevTask.AssignedTo == taskViewModel.AssignedTo)
            {
                _portalRoleService.OverrideTask(_mapper.Map<Tasks>(taskViewModel));
                _context.SaveChanges();
                ViewBag.SuccessMessage = "Task has been updated successfully";
            }

            return View(taskViewModel);
        }

        [HttpGet]
        public IActionResult GenerateReports()
        {
            ViewBag.CurrentPage = "Admin";
            ViewBag.CurrentTab = "GenerateReport";
            return View();
        }

        [HttpGet]
        public IActionResult Api()
        {
            ApiViewmodel api = _mapper.Map<ApiViewmodel>(_portalRoleService.GetApiDetail());
            return View(api);
        }

        [HttpPost]
        public IActionResult Api(ApiViewmodel api)
        {
            if (!ModelState.IsValid)
            {
                var Errors = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .SelectMany(x => x.Value.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                ViewBag.ValidationErrorMessage = Errors;
                return View(api);
            }

            _portalRoleService.UpdateApiDetail(_mapper.Map<DevTaskFlow.Repository_pattern.Core.Enitities.Api>(api));
            ViewBag.SuccessMessage = "Api Updated Successfully";
            return View(api);
        }

        #region Generate excel reports

        [Authorize]
        public IActionResult GetFeedbacks()
        {
            List<Feedback> feedbacks = _portalRoleService.GetFeedbacks();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Feedbacks");

                // Add headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "Message";

                // Fill data
                for (int i = 0; i < feedbacks.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = feedbacks[i].ID;
                    worksheet.Cell(i + 2, 2).Value = feedbacks[i].Name;
                    worksheet.Cell(i + 2, 3).Value = feedbacks[i].Email;
                    worksheet.Cell(i + 2, 4).Value = feedbacks[i].Message;
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"DevTaskFlow-Feedbacks-{DateTime.Now:yyyy-MM-dd_HH:mm}.xlsx";

                return File(
                    stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    excelName
                );
            }
        }

        [Authorize]
        public IActionResult GetErrorLogs()
        {
            List<ErrorLog> errorlogs = _errorService.GetErrorLogs();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ErrorLogs");

                // Add headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Error";
                worksheet.Cell(1, 3).Value = "createdDate";

                // Fill data
                for (int i = 0; i < errorlogs.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = errorlogs[i].ID;
                    worksheet.Cell(i + 2, 2).Value = errorlogs[i].Error;
                    worksheet.Cell(i + 2, 3).Value = errorlogs[i].CreatedDate;
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"DevTaskFlow-ErrorLogs-{DateTime.Now:yyyy-MM-dd_HH:mm}.xlsx";

                return File(
                    stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    excelName
                );
            }
        }

        [Authorize]
        public IActionResult GetApiQuotaDetail()
        {
            ApiViewmodel api = _mapper.Map<ApiViewmodel>(_portalRoleService.GetApiDetail());

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Api");

                // Add headers
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "ApiName";
                worksheet.Cell(1, 3).Value = "Month";
                worksheet.Cell(1, 4).Value = "UsageCount";
                worksheet.Cell(1, 5).Value = "IsActive";
                worksheet.Cell(1, 6).Value = "ModifiedDate";

                // Fill single record
                worksheet.Cell(2, 1).Value = api.Id;
                worksheet.Cell(2, 2).Value = api.ApiName;
                worksheet.Cell(2, 3).Value = api.Month;
                worksheet.Cell(2, 4).Value = api.UsageCount;
                worksheet.Cell(2, 5).Value = api.IsActive;
                worksheet.Cell(2, 6).Value = api.ModifiedDate;

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"DevTaskFlow-ApiDetail-{DateTime.Now:yyyy-MM-dd_HH:mm}.xlsx";

                return File(
                    stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    excelName
                );
            }
        }

        #endregion

        #region Fetching skills & projects & priority & status

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

        /// <summary>
        /// get status list
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> getStatusList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "In-Queue", Value = "In-Queue" },
                new SelectListItem { Text = "Pending", Value = "Pending" },
                new SelectListItem { Text = "InProgress", Value = "InProgress" },
                new SelectListItem { Text = "Completed", Value = "Completed" }
            };
        }

        /// <summary>
        /// get users list
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetusersList()
        {
            var devUsers = _userService.GetDevUsers();
            return devUsers.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.UserName
            }).ToList();
        }

        #endregion
    }
}
