using MbaBlog.Infrastructure;
using MbaBlog.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace MbaBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppIdentityUser _appIdentityUser;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ILogger<HomeController> logger, IAppIdentityUser appIdentityUser)
        {
            _logger = logger;
            _appIdentityUser = appIdentityUser;
        }

        public IActionResult Index()
        {

            var userId = _appIdentityUser.GetUserId();
            var username = _appIdentityUser.GetUsername();
            ViewData["userId"] = _appIdentityUser.GetUserId();
            ViewData["username"] = _appIdentityUser.GetUsername();
            return View();


            //return Content("User");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
