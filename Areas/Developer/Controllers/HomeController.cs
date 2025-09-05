using AutoMapper;
using SkillForgeX.Areas.Developer.ViewModels;
using SkillForgeX.Repository_pattern.Core.Enitities;
using SkillForgeX.Repository_pattern.Repository;
using SkillForgeX.Repository_pattern.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkillForgeX.Areas.Developer.Controllers
{
    [Area("Developer")]
    [Authorize(Roles = "Developer")]
    public class HomeController : Controller
    {
        private readonly PortalRoleService _portalRoleService;
        private readonly UserService _userService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private static readonly object _taskUpdateLock = new object();

        public HomeController(PortalRoleService portalRoleService, UserService userService, AppDbContext appDbContext, IMapper mapper)
        {
            _portalRoleService = portalRoleService;
            _userService = userService;
            _mapper = mapper;
            _context = appDbContext;

        }

        [HttpGet]
        public IActionResult Index()
        {
            string Dev = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int DevID = GetUser();
            List<UserTaskViewModel> taskList = _mapper.Map<List<UserTaskViewModel>>(_portalRoleService.GetDeveloperTask(DevID));
            string DevName = _userService.GetDevUsers().Where(x => x.ID == DevID).Select(x => x.UserName).FirstOrDefault();
            ViewBag.CurrentPage = "Developer";

            foreach (UserTaskViewModel task in taskList)
            {
                task.ProjectName = _portalRoleService.GetProjects().Where(x => x.ID == task.ProjectID).Select(x => x.ProjectName).FirstOrDefault();
            }

            if(taskList.Count == 0)
            {
                TempData["ErrorMessage"] = "Currently you have no ongoing task";
                return RedirectToAction("Dashboard", "Home", new { area = "" });
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
            ViewBag.ListOfStatus = getStatus();
            ViewBag.CurrentPage = "Developer";
            return View(taskList);
        }

        [HttpPost]
        public IActionResult Index(UserTaskViewModel userTask)
        {
            lock (_taskUpdateLock)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    _portalRoleService.UpdateDevTask(_mapper.Map<Tasks>(userTask));

                    if (userTask.Status == "In-Queue" || userTask.Status == "Completed")
                    {
                        int DevID = GetUser();
                        var user = _userService.GetDevUsers().Where(x => x.ID == DevID).FirstOrDefault();
                        user.ConcurrentTask -= 1;
                        user.ModifiedBy = DevID;

                        _userService.UpdateTaskForDevUser(user);
                    }

                    TempData["SuccessMessage"] = $"The {userTask.Title} - task under the {userTask.ProjectName} project status has been updated.";

                    _context.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception eq)
                {
                    transaction.Rollback();
                    TempData["ErrorMessage"] = $"error occured while processing your request - {eq.Message}";
                }
            }

            ViewBag.CurrentPage = "Developer";
            return RedirectToAction("Index");
        } 
        private List<SelectListItem> getStatus()
        {
            var status = new List<SelectListItem>
            {
                new SelectListItem { Text = "In-Queue", Value = "In-Queue" },
                new SelectListItem { Text = "Pending", Value = "Pending" },
                new SelectListItem { Text = "InProgress", Value = "InProgress" },
                new SelectListItem { Text = "Completed", Value = "Completed" }
            };

            return status;
        }

        private int GetUser()
        {
            string Dev = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(Dev);
        }

    }
}
