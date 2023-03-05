using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TaskI.Helpers;
using TaskI.Models;
using TaskI.Services;

namespace TaskI.Controllers
{
    public class ShortUrlsController : Controller
    {
        private readonly IShortUrlService _service;

        public ShortUrlsController(IShortUrlService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var all = _service.GetAll();
            Dictionary<ShortUrl, string> dictionary = new Dictionary<ShortUrl, string>();
            var user = HttpContext.Session.GetInt32("UserId");

            foreach (var item in all)
            {
                dictionary.Add(item, ShortUrlHelper.Shorten(item.OriginalUrl));
            }

            if (user != null)
            {
                var user2 = _service.GetUserById(user.HasValue ? user.Value : 0).isAdmin;
                ViewBag.isAdmin = user2;
            }

            return View(dictionary);


        }
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Create(int Id, string OriginalUrl)
        {
            Regex regex = new Regex(@"(http:)?(https:)?//");

            if (!regex.IsMatch(OriginalUrl.ToString()))
                return RedirectToAction(controllerName: "ShortUrls", actionName: "Error");

            if (_service.GetByOriginalUrl(OriginalUrl) == null)
            {

                var url = new ShortUrl()
                {
                    UserId = Id,
                    CreatedDate = DateTime.Now,
                    OriginalUrl = OriginalUrl
                };

                _service.Save(url);

                return RedirectToAction(controllerName: "ShortUrls", actionName: "Index");
            }
            else
            {
                return RedirectToAction(controllerName: "ShortUrls", actionName: "Error");
            }
        }

        public IActionResult Delete(int id)
        {
            _service.DeleteUrl(id);

            return RedirectToAction(controllerName: "ShortUrls", actionName: "Index");
        }
        public IActionResult Details(int id)
        {
            var item = _service.GetById(id);
            if (item.User == null)
                item.User = _service.GetUserById(item.UserId);
            return View(item);

        }

        [HttpGet("/ShortUrls/RedirectTo/{path:required}", Name = "ShortUrls_RedirectTo")]
        public IActionResult RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = _service.GetByPath(path);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl.OriginalUrl);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
