using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsEditor.Models;
using NewsEditor.Models.DB;

namespace NewsEditor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        MyDBContext context;
        public static HomeController ControllerRef { get; private set; }
        public string Hostname
        {
            get
            {
                Uri requestUri = new Uri(HttpContext.Request.GetDisplayUrl());

                string schemeAndAuthority = requestUri.GetLeftPart(UriPartial.Scheme | UriPartial.Authority);

                return schemeAndAuthority + '/';
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            context = new MyDBContext();
            ControllerRef = this;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetImage(int articleId)
        {
            var article = context.News.FirstOrDefault(article => article.Id == articleId);

            if (article != null && article.Image != null)
            {
                string contentType = $"image/{article.ImageFormat?.ToLower()}";

                // возвращает изображение как поток
                return File(article.Image, contentType);
            }

            return NotFound();
        }

        public IActionResult NewsList()
        {
            return View(context.News.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateArticle(string header, IFormFile image, string subHeader, string text)
        {
            byte[] imageData = null;

            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    image.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }
            //получаем расширение файла без точки
            var imageFormat = Path.GetExtension(image.FileName).Substring(1);
            context.CreateArticle(header, imageData, imageFormat, subHeader, text);

            return RedirectToAction("Index");
        }
        public IActionResult EditNews()
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
