using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Appeal_Web_Application.App_Classes
{
    public class _SecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            if (HttpContext.Current.Session["Kullanici"] == null && controllerName != "Kullanici" && actionName != "Login")
            {
                if (controllerName=="Admin")
                {
                    if (HttpContext.Current.Session["Admin"] == null)
                    {
                        filterContext.Result = new RedirectResult("/Admin/Login");
                    }
                }
                else if(controllerName == "Print" && HttpContext.Current.Session["Admin"] != null)
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    if (actionName != "SifremiUnuttum" || actionName != "GuncelleAdmin")
                    {
                        filterContext.Result = new RedirectResult("/Kullanici/Login");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}