using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EagleEye.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,Inherited =true,AllowMultiple =true)]
    public class CheckAuthorization : AuthorizeAttribute
    {
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null) //|| !HttpContext.Current.Request.IsAuthenticated
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 302; //Found Redirection to another page. Here- login page. Check Layout ajaxError() script.
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Login/Index");

                    //filterContext.Result = new RedirectResult("/Login/Index/ReturnUrl=" +
                    //     filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                }
            }
            else
            {
                //Code HERE for page level authorization

            }
        }
    }
}