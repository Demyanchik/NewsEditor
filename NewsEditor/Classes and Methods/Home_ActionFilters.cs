using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewsEditor.Controllers;

namespace NewsEditor.Methods
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            if (!actionName.Contains("GetImage")) 
            {
                var controller = context.Controller as Controller; // Получаем экземпляр контроллера
                if (controller != null)
                {
                    HomeController.AuthorizationSettings(controller);
                }
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }

    /// <summary>
    /// Фильтр, запрещающий гостям доступ к функционалу администраторов
    /// </summary>
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var IsAuthorized = (bool)controller.ViewBag.IsAuthorized;
            if (!IsAuthorized) 
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }

    public class SetLanguageFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            if (!actionName.Contains("GetImage"))
            {
                var controller = context.Controller as HomeController; // Получаем экземпляр контроллера
                if (controller != null)
                {
                    controller.SetCurrentLanguage();
                }
            }
            base.OnActionExecuting(context);
        }
    }
}