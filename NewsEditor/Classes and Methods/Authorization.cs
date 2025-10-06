using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using NewsEditor.Methods;
using NewsEditor.Models.DB;

namespace NewsEditor.Controllers
{
    public partial class HomeController : Controller
    {
        /// <summary>
        /// Задаем конфигурацию для авторизованного/неавторизованного пользователя
        /// </summary>
        public static void AuthorizationSettings(Controller controller) 
        {
            if (!AuthorizationCheck(controller))
            {
                controller.ViewBag.IsAuthorized = false;
                return;
            }
            controller.ViewBag.IsAuthorized = true;
            controller.ViewBag.AuthorizedUser = GetAuthorizedUser((HomeController)controller);
        }
        int TryLogin(string login, string password)
        {
            var user = context.Users.Where(usr => usr.Login.Equals(login) && usr.Deleted == 0).FirstOrDefault();
            if (user == null)
                return -1;

            var hash = HashHelper.GetHashCode(password + user.TimeCreated);
            if (!user.Password.Equals(hash))
                return -2;

            return 1;
        }
        ///<summary>
        ///Проверка наличия учетных данных пользователя в сессии/куки
        ///</summary>
        static bool AuthorizationCheck(Controller controller)
        {
            if (controller.HttpContext.Session.GetString("login") == null)
            {
                if (controller.Request.Cookies["login"] != null)
                {
                    if (controller.Request.Cookies["login"] == "")
                        return false;
                }
                else
                    return false;
            }

            return true;
        }
        static Users? GetAuthorizedUser(HomeController controller)
        {
            string? login = controller.HttpContext.Session.GetString("login") ?? controller.Request.Cookies["login"];
            return controller.context.Users.Where(usr => usr.Login.Equals(login)).FirstOrDefault();

        }
        void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        void SetCookie(string key, string value)
        {
            Response.Cookies.Append(key, value);
        }
        void SessionDeleteKey(string key)
        {
            HttpContext.Session.Remove(key);
        }
        void CookieDeleteKey(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}
