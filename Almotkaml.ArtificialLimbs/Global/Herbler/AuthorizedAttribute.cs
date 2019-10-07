using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizedAttribute : AuthorizeAttribute
    {

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (Authentication.IsLoggedIn && Authentication.Allowed)
                return;

            if (!Authentication.IsLoggedIn)
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login" })
                    );

            if (Authentication.IsLoggedIn && !Authentication.Allowed)
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "NotAuthorized" })
                    );

        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (Authentication.IsLoggedIn && Authentication.Allowed)
                return true;

            return false;
        }
    }



    public static class Authentication
    {
        public static bool ToLogin { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static bool Allowed { get; set; }
    }
}