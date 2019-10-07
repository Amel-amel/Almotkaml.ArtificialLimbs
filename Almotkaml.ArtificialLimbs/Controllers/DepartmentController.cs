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
    public class DepartmentController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public DepartmentController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Department
        public ActionResult Index()
        {
            var model = Prepare();

            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (model == null)
                return HttpNotFound();

            return View(model);
        }
        
        // GET: Department/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Department_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(Prepare());
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                var department = Department.New(model.DepartmentName);
                _unitOfWork.Department.Add(department);
                _unitOfWork.Complete(p => p.Department_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }
                return View();
            
        }

        // GET: Department/Edit/5
        public ActionResult Edit(DepartmentModel model,int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Department_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = _unitOfWork.Department.GetAll().Where(s => s.DepartmentID == id).ToGrid();
            if (department == null)
            {
                return HttpNotFound();
            }

            foreach (var row in department)
            {
                model.DepartmentName = row.DepartmentName;
                model.DepartmentID = row.DepartmentID;
            }

            return View(model);
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DepartmentModel  model)
        {
            if (ModelState.IsValid)
            {
                var department = _unitOfWork.Department.GetById(model.DepartmentID);
                department.Modify(model.DepartmentName);
                _unitOfWork.Complete(p => p.Department_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id,DepartmentModel model)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Department_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _unitOfWork.Department.GetAll().Where(s => s.DepartmentID == id).ToGrid();

            if (department == null)
            {
                return HttpNotFound();
            }
            foreach (var row in department)
            {
                model.DepartmentName = row.DepartmentName;
                model.DepartmentID = row.DepartmentID;
            }
            return View(model);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var department = _unitOfWork.Department.GetById(id);
            _unitOfWork.Department.Remove(department);
            _unitOfWork.Complete(p => p.Department_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
        => ViewBag.Permissions.Department && permission;

        public DepartmentModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<DepartmentModel>(RequestState.NoPermission);

            return new DepartmentModel()
            {
                CanCreate = ViewBag.Permissions.Department_Create,
                CanEdit = ViewBag.Permissions.Department_Edit,
                CanDelete = ViewBag.Permissions.Department_Delete,
                DepartmentGrid = _unitOfWork.Department.GetAll().ToGrid()
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

    }
}
