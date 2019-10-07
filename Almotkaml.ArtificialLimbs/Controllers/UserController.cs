using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class UserController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UserController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Setting
        public ActionResult Index()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.User_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            var model = Prepare();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_unitOfWork.User.NameExisted(model.UserName.Trim()))
            {
                Note("الاسم الذي أدخلته مسجل مسبقاً .");
                return View(Prepare());
            }
            if (!model.Validate(model))
            {
                Note("كلمة المرور التي أدخلتها غير متطابقة .");
                return View(Prepare());
            }
            var user = Core.Domain.User.New(model.UserName, model.UserTitle, model.Password, model.UserGroupId);
            _context.Users.Add(user);
            _unitOfWork.Complete(p => p.User_Create);
            SuccessCreateNote();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(UserModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.User_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            model = Prepare();
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _unitOfWork.User.GetAll().Where(s => s.UserId == id).ToGrid();

            if (user == null)
            {
                return HttpNotFound();
            }
            foreach (var row in user)
            {
                model.UserID = row.UserID;
                model.UserName = row.UserName;
                model.UserTitle = row.UserTitle;
                model.UserGroupId = row.GroupID;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.User.GetById(model.UserID);
                user.Modify( model.UserTitle,model.UserName, model.Password, model.UserGroupId);
                _unitOfWork.Complete(p => p.User_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(Prepare());
        }

        public ActionResult Delete(UserModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.User_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _unitOfWork.User.GetAll().Where(s => s.UserId == id).ToGrid();

            if (user == null)
            {
                return HttpNotFound();
            }
            foreach (var row in user)
            {
                model.UserID = row.UserID;
                model.UserName = row.UserName;
                model.UserTitle = row.UserTitle;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _unitOfWork.User.GetById(id);
            _unitOfWork.User.Remove(user);
            _unitOfWork.Complete(p => p.User_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
          => ViewBag.Permissions.User && permission;

        public UserModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<UserModel>(RequestState.NoPermission);

            return new UserModel()
            {
                CanCreate = ViewBag.Permissions.User_Create,
                CanEdit = ViewBag.Permissions.User_Edit,
                CanDelete = ViewBag.Permissions.User_Delete,
                UserGrid = _unitOfWork.User.GetAll().ToGrid(),
                UserGroupList = _unitOfWork.UserGroup.GetAll().ToList(),
            };
        }

    }
}
