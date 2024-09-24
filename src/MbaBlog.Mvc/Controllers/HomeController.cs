using MbaBlog.Infrastructure.Repositories;
using MbaBlog.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace MbaBlog.Mvc.Controllers
{
    public class HomeController() : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index","Posts");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
