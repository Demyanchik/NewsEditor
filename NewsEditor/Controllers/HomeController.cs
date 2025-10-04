using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsEditor.Models;
using NewsEditor.Models.DB;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;

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

        int Index_slider_news_count { get; set; } = 5;
        int NewsList_start_news_count { get; set; } = 5;
        int NewsList_loading_news_count { get; set; } = 2;

        public HomeController(ILogger<HomeController> logger, IRazorViewEngine viewEngine)
        {
            _logger = logger;

            context = new MyDBContext();
            ControllerRef = this;
        }

        public IActionResult LogIn() 
        {
            return View();
        }

        public IActionResult Index()
        {
            var news = context.News.ToList();
            news.Reverse();

            return View(news.Take(Index_slider_news_count).ToList());
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
            var news = context.News.Where(news => news.Deleted == 0).ToList();
            news.Reverse();

            var top_news = news.Take(NewsList_start_news_count).ToList();

            return View(top_news);
        }

        public IActionResult GetNextNews(int lastShownArticleId = -1)
        {
            if (lastShownArticleId < 0)
                return PartialView("~/Views/UI_Components/GetNextNews.cshtml", null);

            var selectedNews = context.News.ToList();
            selectedNews.Reverse();

            var lastShownArticle = selectedNews.Find(a => a.Id == lastShownArticleId);
            var startIndex = selectedNews.IndexOf(lastShownArticle);

            var listAfter = selectedNews.Skip(startIndex + 1);
            var resultNews = listAfter.Where(a => a.Deleted == 0).Take(NewsList_loading_news_count);

            return PartialView("~/Views/UI_Components/GetNextNews.cshtml", resultNews);
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

            return RedirectToAction("NewsList");
        }

        public IActionResult UpdateArticle(int newsId, string header, string hasImage, IFormFile changedImage, string subHeader, string text) 
        {
            context.UpdateArticle(newsId, header, hasImage, changedImage, subHeader, text);

            return RedirectToAction("NewsList");
        }
        public IActionResult DeleteArticle(int id) 
        {
            context.DeleteArticle(id);

            return RedirectToAction("NewsList");
        }
        public IActionResult CreateNews()
        {
            return View();
        }
        public IActionResult Article(int id) 
        {
            var article = context.News.Find(id);
            if (article != null && article.Deleted == 0)
            {
                return View(article);
            }

            return NotFound();
        }
        public IActionResult EditNews(int newsId)
        {
            var article = context.News.Find(newsId);
            if (article != null && article.Deleted == 0) 
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
