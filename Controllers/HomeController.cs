using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevTaskFlow.Models;
using DevTaskFlow.ViewModels;
using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Repository_pattern.Service.Services;
using AutoMapper;


namespace DevTaskFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly PortalRoleService _portalRoleService;
        private readonly ErrorService _errorService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, UserService userService, PortalRoleService portalRoleService, ErrorService errorService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _portalRoleService = portalRoleService;
            _errorService = errorService;
            _mapper = mapper;
        }

        #region login

        [HttpGet]
        public IActionResult Login()
        {
            UserTypeViewModel usertype = new UserTypeViewModel();
            return View(usertype);
        }

        [HttpPost]
        public IActionResult Login(UserTypeViewModel userType)
        {
            return RedirectToAction("Signup", new { isRegisterUser = userType.IsRegisteredUser });
        }

        [HttpGet]
        public IActionResult Signup(bool isRegisterUser)
        {
            LoginViewModel login = new LoginViewModel();
            login.isRegisteredUser = isRegisterUser;
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(LoginViewModel login)
        {
            if (!login.isRegisteredUser)
                ModelState.Remove("Password");

            if(!ModelState.IsValid)
            {
                var Errors = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .SelectMany(x => x.Value.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                ViewBag.ValidationErrorMessage = Errors;
                return View(login);
            }

            UserModel currentUser;

            if (login.isRegisteredUser)
            {
                var user = _userService.GetUsers()
                        .FirstOrDefault(x => x.UserName == login.UserName && x.Password == login.Password);

                if (user == null)
                {
                    ViewBag.ErrorMessage = "Invalid username or password!";
                    return View(login);
                }

                currentUser = new()
                {
                    UserName = user.UserName,
                    RoleID = user.UserRole,
                    Role = ((Models.PortalRoles)user.UserRole).ToString(),
                    UserID = user.ID
                };
            }
            else
            {
                currentUser = new()
                {
                    UserName = login.UserName,
                    RoleID = 4,
                    Role = ((Models.PortalRoles) 4).ToString(),
                    UserID = 0
                };

                //adding guestuser
                _logger.LogInformation($"new guest user - {login.UserName} was logged in @ {DateTime.Now}");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(ClaimTypes.Role, currentUser.Role),
                new Claim(ClaimTypes.NameIdentifier, currentUser.UserID.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign in user
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

            return RedirectToAction("Dashboard");
        }

        #endregion

        [Authorize]
        public IActionResult Dashboard()
        {
            ViewBag.NoOfTasks = _portalRoleService.GetTaskCount();
            ViewBag.NoofUsers = _userService.GetDevUserCount();
            ViewBag.CurrentPage = "Dashboard";
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Feedback()
        {
            ViewBag.CurrentPage = "Feedback";
            FeedbackViewModel feedback = new FeedbackViewModel();
            return View(feedback);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Feedback(FeedbackViewModel feedback)
        {
            if (!ModelState.IsValid)
            {
                var Errors = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .SelectMany(x => x.Value.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                ViewBag.ValidationErrorMessage = Errors;
                return View(feedback);
            }

             _portalRoleService.UpdateFeedback(_mapper.Map<Feedback>(feedback));
            ViewBag.SuccessMessage = "Your response has been sent";
            return View(feedback);
        }

        [Authorize]
        public IActionResult ProjectOverview()
        {
            ViewBag.CurrentPage = "ProjectOverview";
            return View();
        }

        [Authorize]
        public IActionResult Notifications()
        {
            ViewBag.CurrentUser = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.CurrentPage = "Notifications";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user and clear the authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Route("Home/UnauthorizedAccess")]
        [Authorize]
        public IActionResult UnauthorizedAccess()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Home/Error")]
        [Authorize]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            string error = string.Format("{0} - {1}",
                                feature?.Error?.Message,
                                feature?.Path ?? "unknown");

            _errorService.AddError(error);


            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = error
            });
        }
    }
}
