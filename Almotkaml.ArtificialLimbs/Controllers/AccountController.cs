using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.ArtificialLimbs.Global.Herbler;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Core;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    [Authorized]
    public class AccountController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        
        public AccountController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public RedirectToRouteResult Index()
        {
            return RedirectToAction(nameof(HomeController.Index), ControllerName(nameof(HomeController)));
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Authentication.IsLoggedIn)
                return RedirectToAction("Index");

            ViewBag.ReturnUrl = returnUrl;
            var model = prepare();

            return View(model);
        }


        ////
        //// POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // returnUrl = Convert.ToString(Request.QueryString["url"]);
            if (Authentication.IsLoggedIn)
                return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "يرجى ادخال اسم المستخدم وكلمة المرور !");
                return View(model);
            }

            if (!IsLogin(model))
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صحيحة!");
                return View(model);
            }

            SessionManager.Set(model);
            Authentication.IsLoggedIn = true;
            Authentication.Allowed = true;

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), ControllerName(nameof(HomeController)));
            }

        }


        public RedirectToRouteResult Logout()
        {
            Session.Clear();
            Authentication.IsLoggedIn = false;
            return RedirectToAction(nameof(System.Web.UI.WebControls.Login));
        }


        public bool IsLogin(LoginModel model)
        {
            if (!ModelState.IsValid)
                return false;
            // var model2 = new HomeModel();
            ViewBag.Permissions = _unitOfWork.User.GetByNameAndPassword(model);
            //  model.CheckUserPerm = user.CheckUserPerm;

            return ViewBag.Permissions != null;//|| LoginFailed(m => model.UserName);
        }


        private LoginModel prepare()
        {
            return new LoginModel
            {

            };
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }

}