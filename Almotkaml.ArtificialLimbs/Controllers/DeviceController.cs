using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class DeviceController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public DeviceController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Device
        public ActionResult Index()
        {
            var model = Prepare();

            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (model == null)
                return HttpNotFound();

            return View(model);
        }
        
        // GET: Device/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Device_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(Prepare());
        }

        // POST: Device/Create
        [HttpPost]
        public ActionResult Create(DeviceModel model)
        {
            if (_unitOfWork.Device.DeviceExist(model.ArabicName))
            {
                Note("الجهاز الذي أدخلته موجود مسبقا .");
                return View(Refresh(model));
            }

            if (ModelState.IsValid)
            {
                var device = Device.New(model.ArabicName,model.EnglishName,model.DepartmentID,model.Type);
                _unitOfWork.Device.Add(device);
                _unitOfWork.Complete(p => p.Device_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Device/Edit/5
        public ActionResult Edit(int id,DeviceModel model)
        {
            model = Refresh(model);
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Device_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var device = _unitOfWork.Device.GetAll().Where(s => s.DeviceID == id).ToGrid();
            if (device == null)
            {
                return HttpNotFound();
            }

            foreach (var row in device)
            {
                model.DeviceID = row.DeviceID;
                model.EnglishName = row.EnglishName;
                model.ArabicName = row.ArabicName;
                model.Type  = row.Type;
                model.DepartmentID = row.DepartmentID;
            }

            return View(model);
        }

        // POST: Device/Edit/5
        [HttpPost]
        public ActionResult Edit(DeviceModel model)
        {
            if (ModelState.IsValid)
            {
                var device = _unitOfWork.Device.GetById(model.DepartmentID);
                device.Modify(model.ArabicName,model.EnglishName,model.DepartmentID,model.Type);
                _unitOfWork.Complete(p => p.Device_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }

            return View(Refresh(model));
        }

        // GET: Device/Delete/5
        public ActionResult Delete(int id, DeviceModel model)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Device_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var device = _unitOfWork.Device.GetAll().Where(s => s.DeviceID == id).ToGrid();
            if (device == null)
            {
                return HttpNotFound();
            }

            foreach (var row in device)
            {
                model.DeviceID = row.DeviceID;
                model.ArabicName = row.ArabicName;
                model.EnglishName = row.EnglishName;
                model.Type = row.Type;
                model.DepartmentID = row.DepartmentID;
            }

            return View(model);

        }

        // POST: Device/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var device = _unitOfWork.Device.GetById(id);
            _unitOfWork.Device.Remove(device);
            _unitOfWork.Complete(p => p.Device_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
       => ViewBag.Permissions.Device && permission;

        public DeviceModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<DeviceModel>(RequestState.NoPermission);

            return new DeviceModel()
            {
                CanCreate = ViewBag.Permissions.Device_Create,
                CanEdit = ViewBag.Permissions.Device_Edit,
                CanDelete = ViewBag.Permissions.Device_Delete,
                DeviceGrid = _unitOfWork.Device.GetAll().ToGrid(),
                DepartmentList = _unitOfWork .Department .GetAll().ToList(),
            };
        }

        public DeviceModel Refresh(DeviceModel model)
        {
            GetPermission();
            if (!HavePermission())
                return Null<DeviceModel>(RequestState.NoPermission);

            model.CanCreate = ViewBag.Permissions.Device_Create;
            model.CanEdit = ViewBag.Permissions.Device_Edit;
            model.CanDelete = ViewBag.Permissions.Device_Delete;
            
            model.DepartmentList = _unitOfWork.Department.GetAll().ToList();

            return model;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
