using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Linq;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class EmployeeController : BaseController
    {
        AlmotkamlHrEntities1 _db = new AlmotkamlHrEntities1();
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public EmployeeController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Employee
        public ActionResult Index()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Device_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(Prepare());
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Device_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(Prepare());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection form, EmployeeModel model)
        {
            //model = Refresh(model);
            if (IntValue(form["Selected"]) > 0)
                model.HREmpID = IntValue(form["Selected"]);
            string FullName = "";
            if (model.HREmpID > 0)
            {
                Core.Linq.Employee employee = _db.Employees.Find(model.HREmpID);

                if (employee == null)
                {
                    return HttpNotFound();
                }

                FullName = employee.FirstName + " " + employee.FatherName + " " + employee.LastName;
                if (_unitOfWork.Employee.NameExist(FullName))
                {
                    Note("الاسم الذي قمت باختياره تم إختياره مسبقا!");
                    return View(Refresh(model));
                }
                model.EmployeeName = FullName;
            }

            if (form["Save"] != null)

                if (ModelState.IsValid)
                {
                    var employee = Core.Domain.Employee.New(model.EmployeeName, model.DepartmentID);
                    _unitOfWork.Employee.Add(employee);
                    _unitOfWork.Complete(p => p.Employee_Create);
                    SuccessCreateNote();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (model.EmployeeName != "" && model.DepartmentID > 0)
                    {
                        var employee = Core.Domain.Employee.New(model.EmployeeName, model.DepartmentID);
                        _unitOfWork.Employee.Add(employee);
                        _unitOfWork.Complete(p => p.Employee_Create);
                        SuccessCreateNote();
                        return RedirectToAction("Index");

                    }

                }

            return View(Refresh(model));
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id, EmployeeModel model)
        {
            model = Refresh(model);
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Employee_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = _unitOfWork.Employee.GetAll().Where(s => s.EmployeeID == id).ToGrid();
            if (employee == null)
            {
                return HttpNotFound();
            }

            foreach (var row in employee)
            {
                model.EmployeeID = row.EmployeeID;
                model.EmployeeName = row.EmployeeName;
                model.DepartmentID = row.DepartmentID;
            }

            return View(model);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _unitOfWork.Employee.GetById(model.EmployeeID);
                employee.Modify(model.DepartmentID);
                _unitOfWork.Complete(p => p.Employee_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }

            return View(Refresh(model));
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id, EmployeeModel model)
        {
            model = Refresh(model);
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Employee_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = _unitOfWork.Employee.GetAll().Where(s => s.EmployeeID == id).ToGrid();
            if (employee == null)
            {
                return HttpNotFound();
            }

            foreach (var row in employee)
            {
                model.EmployeeID = row.EmployeeID;
                model.EmployeeName = row.EmployeeName;
                model.DepartmentID = row.DepartmentID;
            }

            return View(model);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = _unitOfWork.Employee.GetById(id);
            _unitOfWork.Employee.Remove(employee);
            _unitOfWork.Complete(p => p.Employee_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
     => ViewBag.Permissions.Device && permission;

        public EmployeeModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<EmployeeModel>(RequestState.NoPermission);

            return new EmployeeModel()
            {
                CanCreate = ViewBag.Permissions.Employee_Create,
                CanEdit = ViewBag.Permissions.Employee_Edit,
                CanDelete = ViewBag.Permissions.Employee_Delete,
                EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid(),
                DepartmentList = _unitOfWork.Department.GetAll().ToList(),
                HREmployeeGrid = _db.Employees.ToList(),
            };
        }

        public EmployeeModel Refresh(EmployeeModel model)
        {
            GetPermission();
            if (!HavePermission())
                return Null<EmployeeModel>(RequestState.NoPermission);

            model.CanCreate = ViewBag.Permissions.Employee_Create;
            model.CanEdit = ViewBag.Permissions.Employee_Edit;
            model.CanDelete = ViewBag.Permissions.Employee_Delete;

            model.DepartmentList = _unitOfWork.Department.GetAll().ToList();
            model.HREmployeeGrid = _db.Employees.ToList();
            if (model.EmployeeName != "") model.EmployeeName = model.EmployeeName;
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
