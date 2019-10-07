using Almotkaml.ArtificialLimbs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Global
{
    public static class SessionManager
    {
        public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static int Year { get; set; }
        public static string Permissions { get; set; }
        public static void Load()
        {
            try
            {
                UserName = (string)HttpContext.Current.Session["UserName"];
                Password = (string)HttpContext.Current.Session["Password"];
                //Year = (int)HttpContext.Current.Session["Year"];
            }
            catch
            {
                // ignored
            }
           
        }

        public static void Set(LoginModel model)
        {
            HttpContext.Current.Session["UserName"] = UserName = model.UserName;
            HttpContext.Current.Session["Password"] = Password = model.Password;

        }

    }
}