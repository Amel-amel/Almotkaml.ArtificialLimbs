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
    public class UserGroupController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UserGroupController()
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
            if (!HavePermission(ViewBag.Permissions.UserGroup_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            var model = Prepare();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserGroupModel usergroup)
        {
            if (!ModelState.IsValid)
            {
                return View(usergroup);
            }
            if (_unitOfWork.UserGroup.NameExisted(usergroup.GroupTitle.Trim()))
            {
                Note("الاسم الذي أدخلته مسجل مسبقاً .");
                return View(usergroup);
            }

            string Per = new JavaScriptSerializer().Serialize(usergroup.Permission);
            var userGroup = UserGroup.New(usergroup.GroupTitle, Per);

            _context.UserGroups.Add(userGroup);
            _unitOfWork.Complete(p => p.UserGroup_Create);
            SuccessCreateNote();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(UserGroupModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.UserGroup_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userGroup = _unitOfWork.UserGroup.GetAll().Where(s => s.UserGroupId == id).ToGrid();

            if (userGroup == null)
            {
                return HttpNotFound();
            }
            foreach (var row in userGroup)
            {
                model.UserGroupID = row.UserGroupID;
                model.GroupTitle = row.GroupTitle;

                JavaScriptSerializer java = new JavaScriptSerializer();
                model.Permission = java.Deserialize<Permission>(row._Permission);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserGroupModel model)
        {
            if (ModelState.IsValid)
            {
                string _permission = new JavaScriptSerializer().Serialize(model.Permission);

                var userGroup = _unitOfWork.UserGroup.GetById(model.UserGroupID);
                userGroup.Modify(model.GroupTitle, _permission);
                _unitOfWork.Complete(p => p.UserGroup_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(UserGroupModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.UserGroup_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userGroup = _unitOfWork.UserGroup.GetAll().Where(s => s.UserGroupId == id).ToGrid();

            if (userGroup == null)
            {
                return HttpNotFound();
            }
            foreach (var row in userGroup)
            {
                model.UserGroupID = row.UserGroupID;
                model.GroupTitle = row.GroupTitle;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userGroup = _unitOfWork.UserGroup.GetById(id);
            _unitOfWork.UserGroup.Remove(userGroup);

            _unitOfWork.Complete(p => p.UserGroup_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        public void test(UserGroupModel usergroup)
        {
            usergroup.GroupTitle = "TestGroup";
            //usergroup.Permission.GeneralSettings  = true ;
            //usergroup.Permission.Setting_Edit = true;
            usergroup.Permission.CompanyInfo_Edit = true;
            usergroup.Permission.CompanyInfo = true;
            string Per = new JavaScriptSerializer().Serialize(usergroup.Permission);
            var userGroup = UserGroup.New(usergroup.GroupTitle, Per);

            _context.UserGroups.Add(userGroup);
            _context.SaveChanges();

            JavaScriptSerializer java = new JavaScriptSerializer();
            usergroup.Permission = java.Deserialize<Permission>(userGroup._Permissions);

        }

        private bool HavePermission(bool permission = true)
         => ViewBag.Permissions.UserGroup && permission;

        public UserGroupModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<UserGroupModel>(RequestState.NoPermission);

            return new UserGroupModel()
            {
                CanCreate = ViewBag.Permissions.UserGroup_Create,
                CanEdit = ViewBag.Permissions.UserGroup_Edit,
                CanDelete = ViewBag.Permissions.UserGroup_Delete,
                UserGroupGrid = _unitOfWork.UserGroup.GetAll().ToGrid(),
                Permission = new Global.Permission(),

            };
        }

    }
}
