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
            var article = context.News.Find(articleId);

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
            var news = context.News.ToList();
            news.Reverse();

            return View(news);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public static byte[]? GetImageBytes(IFormFile image) 
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

            return imageData;
        }
        /// <summary>
        /// Получаем расширение файла без точки
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string? GetImageExtension(IFormFile image)
        {
            return (image == null) ? null : Path.GetExtension(image?.FileName)?.Substring(1);
        }

        public IActionResult CreateArticle(string header, IFormFile image, string subHeader, string text)
        {
            context.CreateArticle(header, image, subHeader, text);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateArticle(int newsId, string header, string hasImage, IFormFile changedImage, string subHeader, string text) 
        {
            context.UpdateArticle(newsId, header, hasImage, changedImage, subHeader, text);

            return RedirectToAction("NewsList");
        }
        public IActionResult CreateNews()
        {
            return View();
        }
        public IActionResult EditNews(int newsId)
        {
            var article = context.News.Find(newsId);
            if (article != null) 
            {
                return View(article);
            }

            return NotFound();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
