using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsEditor.Models;
using NewsEditor.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NewsEditor.Methods;

namespace NewsEditor.Controllers
{
    [AuthorizationFilter]
    [SetLanguageFilter]
    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        internal MyDBContext context;
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
        Dictionary<string, string> CurrentLanguage { get; set; } = LangPacks.Russian;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            context = new MyDBContext();
            ControllerRef = this;
        }
        public void SetCurrentLanguage() 
        {
            int languageId;
            CurrentLanguage = GetCurrentLanguage(out languageId);
            var postfix = context.Languages.Find(languageId).Language;

            ViewBag.CurrentLanguageName = CurrentLanguage["language_name_" + postfix];
            ViewBag.CurrentLanguageId = languageId;
            ViewBag.Languages = context.Languages.ToList();

            ViewBag.LangPack = CurrentLanguage;
        }
        Dictionary<string, string> GetCurrentLanguage(out int langId) 
        {
            langId = 1;
            var languageId = Request.Cookies["LanguageId"];
            if (!string.IsNullOrEmpty(languageId)) 
                return LangPacks.GetLanguagePack(langId = int.Parse(languageId));
            
            var IsAuthorized = (bool)ViewBag.IsAuthorized;
            if (IsAuthorized)
            {
                var user = (Users)ViewBag.AuthorizedUser;
                var user_language = user.Language;
                if(user_language != null)
                    return LangPacks.GetLanguagePack(langId = (int)user_language);
            }
            
            return LangPacks.GetLanguagePack();
        }
        public IActionResult LogIn() 
        {
            try
            {
                Request.Form.Count();
            }
            catch (Exception err)
            {
                return View();
            }

            var login = Request.Form["login"];
            var password = Request.Form["password"];
            int status;

            if ((status = TryLogin(login, password)) > 0)
            {
                //сохранение данных в сессии или куки
                if (Request.Form["stay"].Count() > 0)
                    SetCookie("login", login);
                else
                    SetSession("login", login);

                return RedirectToAction("Index");
            }

            ViewBag.login = login;
            ViewBag.password = password;
            ViewBag.status = status;

            return View();
        }
        public IActionResult LogOut()
        {
            CookieDeleteKey("login");
            SessionDeleteKey("login");

            return RedirectOnPreviousPage();
        }
        void setAuthorizedUserLanguage(int LanguageId)
        {
            if ((bool)ViewBag.IsAuthorized) 
            {
                var user = (Users)ViewBag.AuthorizedUser;
                user.Language = LanguageId;
                context.SaveChanges();
            }
        }
        public IActionResult SetLanguage(int LanguageId) 
        {
            if(context.Languages.Find(LanguageId) == null)
                return RedirectOnPreviousPage();

            SetCookie("LanguageId", LanguageId.ToString());
            
            var IsAuthorized = (bool)ViewBag.IsAuthorized;
            if (IsAuthorized)
            {
                setAuthorizedUserLanguage(LanguageId);
            }
            
            return RedirectOnPreviousPage();
        }


        public IActionResult Index()
        {
            //устанавливаем user-у язык из куки
            var languageId = Request.Cookies["LanguageId"];
            if (!string.IsNullOrEmpty(languageId))
            {
                setAuthorizedUserLanguage(int.Parse(languageId));
            }

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
        [AdminFilter]
        public IActionResult CreateArticle(string header, IFormFile image, string subHeader, string text)
        {
            context.CreateArticle(header, image, subHeader, text);

            return RedirectToAction("NewsList");
        }
        [AdminFilter]
        public IActionResult UpdateArticle(int newsId, string header, string hasImage, IFormFile changedImage, string subHeader, string text) 
        {
            context.UpdateArticle(newsId, header, hasImage, changedImage, subHeader, text);

            return RedirectToAction("NewsList");
        }
        [AdminFilter]
        public IActionResult DeleteArticle(int id) 
        {
            context.DeleteArticle(id);

            return RedirectToAction("NewsList");
        }
        [AdminFilter]
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
        [AdminFilter]
        public IActionResult EditNews(int newsId)
        {
            var article = context.News.Find(newsId);
            if (article != null && article.Deleted == 0) 
            {
                return View(article);
            }

            return NotFound();
            
        }

        public IActionResult RedirectOnPreviousPage(string DefaultAction = "Index") 
        {
            string previousUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(previousUrl))
            {
                // Перенаправляем на предыдущую страницу
                return Redirect(previousUrl);
            }
            return RedirectToAction(DefaultAction);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
