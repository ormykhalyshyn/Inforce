using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskI.Models;
using TaskI.Services;

namespace TaskI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShortUrlService _service;

        public HomeController(ILogger<HomeController> logger, IShortUrlService service)
        {
            _logger = logger;
            _service = service;

        }

        public IActionResult Index()
        {
            return RedirectToAction(controllerName: "ShortUrls", actionName: "Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var user = _service.GetUsersByUsername(Username);
            if (user.Password == Password)
            {
                HttpContext.Session.SetInt32("UserId",user.UserId);


                return RedirectToAction(controllerName: "ShortUrls", actionName: "Index");
            }
            else
            {
                return RedirectToAction(controllerName: "Home", actionName: "Error");
            }
        }

        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Register(string Username, string Password)
        {
            Users user = new Users();

            if (_service.GetUsersByUsername(Username) == null)
            {

                user.Username = Username;
                user.isAdmin = false;
                user.Password = Password;
                _service.SaveNewUser(user);
                return RedirectToAction(controllerName: "Home", actionName: "Login");

            }
            else
            {
                return RedirectToAction(controllerName: "Home", actionName: "Error");
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult LogOut()
        {

            HttpContext.Session.Remove("UserId");
            return RedirectToAction(controllerName: "Home", actionName: "Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}