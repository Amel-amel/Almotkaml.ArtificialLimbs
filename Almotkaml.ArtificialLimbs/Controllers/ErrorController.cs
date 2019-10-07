using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index()
        {
            GetPermission();
            return View();
        }

        // GET: Error
        public ActionResult PageNotFound()
        {
            GetPermission();
            return View();
        }
    }
}
