using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Resources;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Global.Herbler;
using Almotkaml.ArtificialLimbs.Models;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    [Authorized]
   public abstract class BaseController : Controller
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public string Message { get; set; }

        protected BaseController()
        {

            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);

            if (Controller == "Account" && Action == "Login")
            {
                Authentication.ToLogin = true;
            }

            LoginModel loginModel = null;

            try
            {
                SessionManager.Load();
                loginModel = new LoginModel()
                {
                    UserName = SessionManager.UserName,
                    Password = SessionManager.Password,
                };


            }

            catch
            {
                RedirectToLogin();
            }
            if (loginModel.UserName == null)
                RedirectToLogin();

            ShowCategory();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ShowCategory()
        {
            ViewData["CategoryMode"] = System.Web.HttpContext.Current?.Request.QueryString["c"];
            ViewData["ActiveController"] = Controller;
            ViewData[Controller] = "active";
        }

        protected ActionResult AjaxNotWorking() => new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #region helpers

        protected string Controller { get; } = HtmlRequestHelper.Controller(null);
        protected string Action { get; } = HtmlRequestHelper.Action(null);
        protected string Id { get; } = HtmlRequestHelper.Id(null);

        private ActionResult RedirectToLogin()
        {
            Authentication.IsLoggedIn = false;
            return RedirectToAction("Login", "Account");
        }

        protected void GetPermission()
        {
            ViewBag.Permissions = _unitOfWork.User.GetUserPermissions((string)Session["UserName"], (string)Session["Password"]);
        }

        protected TModel Null<TModel>(RequestState state) where TModel : class
        {

            ViewBag.RequestState = state;
            return null;
        }

        private ContentResult NoPermission()
        {
            Authentication.Allowed = false;
            return Content("ليست لديك الصلاحية لدخول هذه الصفحة");
        }

        private void IsSigned()
        {
            Authentication.IsLoggedIn = true;
            Authentication.Allowed = true;
        }

        protected void CallRedirect(long modelId = 0)
        {
            ViewData["Done"] = "Done";
            ViewData["ModelId"] = modelId;
            TempData["Note"] = Message;
        }

        protected void CallRedirect(bool forceRedirect)
        {
            ViewData["Done"] = forceRedirect ? "fDone" : "Done";
            TempData["Note"] = Message;
        }

        protected void SuccessCreateNote()
        {
            ViewBag.RequestState = RequestState.Valid;
            TempData["Note"] = SharedMessages.SuccessCreate;
        }
        protected void SuccessEditNote()
        {
            ViewBag.RequestState = RequestState.Valid;
            TempData["Note"] = SharedMessages.SuccessEdit;
        }
        protected void SuccessDeleteNote()
        {
            ViewBag.RequestState = RequestState.Valid;
            TempData["Note"] = SharedMessages.SuccessDelete;
        }

        protected void Note(string message)
        {
            ViewBag.RequestState = RequestState.Invalid;
            TempData["Note"] = message;
        }

        //protected bool Fail(string message)
        //{
        //    ViewBag.RequestState = RequestState.Invalid;
        //    ViewBag.Message = message;
        //    return false;
        //}

        protected void SaveModel(object model)
        {
            ViewData["SerializedModel"] = JsonConvert.SerializeObject(model);
        }

        protected TModel LoadSavedModel<TModel>(string savedModel)
        {
            return JsonConvert.DeserializeObject<TModel>(savedModel);
        }

        protected string ControllerName(string controller)
        {
            return controller.Replace("Controller", "");
        }

        protected void ClearModelState(Type properType, string propertyName, bool closePanel = false)
        {
            foreach (var property in properType.GetProperties())
            {
                if (ModelState.Keys.Contains(propertyName + "." + property.Name))
                    ModelState[propertyName + "." + property.Name].Errors.Clear();
            }

            if (closePanel)
                ClosePanel();
        }

        protected long LongValue(string value)
        {
            long number;
            return long.TryParse(value, out number) ? number : 0;
        }
        protected int IntValue(string value)
        {
            int number;
            return int.TryParse(value, out number) ? number : 0;
        }

        protected void ClosePanel()
        {
            ViewData["PanelStateOpened"] = "none";
            ViewData["PanelStateClosed"] = "";
        }

        protected void OpenPanel()
        {
            ViewData["PanelStateOpened"] = "";
            ViewData["PanelStateClosed"] = "none";
        }


        #endregion


    }
}
