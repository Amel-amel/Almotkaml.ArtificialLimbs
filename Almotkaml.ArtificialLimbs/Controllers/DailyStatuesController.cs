using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Enum;
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
    public class DailyStatuesController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public DailyStatuesController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: DailyStatues
        public ActionResult Index()
        {
            var model = Prepare();
            //model.DailyStatuesGrid = _unitOfWork.DailyStatues.GetAll();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View(model);
        }

        // POST : DailyStatues
        [HttpPost]
        public ActionResult Index(FormCollection form, DailyStatuesModel model)
        {
            var recipt = IntValue(form["Receipt"]);
            var state = form["State"];
            var searchVal = IntValue(form["Searchbtn"]);
            if (recipt > 0)
            {
                if (CheckData(recipt))
                {
                    return View(Refresh(model));
                }
                else
                {
                    return RedirectToAction("Edit", new { id = recipt });
                }
            }
            if (searchVal > 0)
            {
                model.DailyStatuesGrid = _unitOfWork.DailyStatues.Search(model, state).ToGrid();
            }
            return View(Refresh(model));
        }

        // GET: DailyStatues/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyStatues_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View(Prepare());
        }

        // POST: DailyStatues/Create
        [HttpPost]
        public ActionResult Create(FormCollection form, DailyStatuesModel model)
        {
            if (model.SetRefNO && model.ReferenceNo == null)
                model.ReferenceNo = SetRefNO(model);
            if (form["Save"] != null)
                if (ModelState.IsValid)
                {
                    var dailyStatues = DailyStatues.New(model.PatientID, model.TechnicalID, model.TechnicalAssistantID, model.DeviceID, model.MeasurementDate, model.ReceiptDate, model.ReferenceNo, model.Note);
                    _unitOfWork.DailyStatues.Add(dailyStatues);
                    _unitOfWork.Complete(p => p.DailyStatues_Create);
                    SuccessCreateNote();
                    return RedirectToAction("Index");
                }

            return View(Refresh(model));
        }

        //Get : تسجيل حالة جديدة
        public ActionResult NewStatues()
        {
            var model = Prepare();
            model.DeviceList = _unitOfWork.Device.GetAll().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult NewStatues(DailyStatuesModel model)
        {

            var device = _unitOfWork.Device.GetById(model.DeviceID);//.Where(s => s.DeviceID == id);


            if (device != null && model.PatientID != 0)
            {

                if (device.Type == Types.Daily)
                {
                    int StatuesId = _unitOfWork.Patient.AddNewStatues(model.PatientID, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(DailyStatuesController.Edit), ControllerName(nameof(DailyStatuesController)), routeValues: new { id = StatuesId });
                }
                else
                {
                    int StatuesId = _unitOfWork.Patient.AddNewStatues(model.PatientID, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(AmputationStatuesController.Edit), ControllerName(nameof(AmputationStatuesController)), routeValues: new { id = StatuesId });
                }
            }
            return View(Refresh(model));
        }


        // GET: DailyStatues/Edit/5
        public ActionResult Edit(int id, DailyStatuesModel model)
        {
            model = Refresh(model);

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyStatues_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dailyStatues = _unitOfWork.DailyStatues.GetAll().Where(s => s.DailyStatuesID == id).ToGrid();

            if (dailyStatues == null)
            {
                return HttpNotFound();
            }
            foreach (var row in dailyStatues)
            {
                model.DailyStatuesID = row.DailyStatuesID;
                model.PatientName = row.PatientName;
                model.TechnicalName = row.TechnicalName;
                model.AssistantName = row.AssistantName;
                model.DeviceID = row.DeviceID;
                model.MeasurementDate = row.MeasurementDate;
                model.ReceiptDate = row.ReceiptDate;
                model.Note = row.Note;
                model.PatientID = row.PatientIID;
                model.TechnicalID = row.TechnicalID;
                model.ReferenceNo = row.ReferenceNo;
                model.TechnicalAssistantID = row.TechnicalAssistantID;
            }

            return View(Refresh(model));
        }

        // POST: DailyStatues/Edit/5
        [HttpPost]
        public ActionResult Edit(DailyStatuesModel model, FormCollection form)
        {
            var value = form["Save"];

            if (model.SetRefNO && model.ReferenceNo == null)
                model.ReferenceNo = SetRefNO(model);

            if (model.ReceiptDate != null)
                if (!CheckData(model.DailyStatuesID, model))
                {
                    return View(Refresh(model));
                }

            if (value != null)
                if (ModelState.IsValid)
                {
                    var dailyStatues = _unitOfWork.DailyStatues.GetById(model.DailyStatuesID);
                    dailyStatues.Modify(model.PatientID, model.TechnicalID, model.TechnicalAssistantID, model.DeviceID, model.MeasurementDate, model.ReceiptDate, model.ReferenceNo, model.Note);
                    SuccessEditNote();
                    _unitOfWork.Complete(p => p.DailyStatues_Edit);
                    return RedirectToAction("Index");
                }
            return View(Refresh(model));
        }

        // GET: DailyStatues/Delete/5
        public ActionResult Delete(int id, DailyStatuesModel model)
        {
            model = Refresh(model);
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyStatues_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dailyStatues = _unitOfWork.DailyStatues.GetAll().Where(s => s.DailyStatuesID == id).ToGrid();

            if (dailyStatues == null)
            {
                return HttpNotFound();
            }
            foreach (var row in dailyStatues)
            {
                model.DailyStatuesID = row.DailyStatuesID;
                model.PatientName = row.PatientName;
                model.TechnicalName = row.TechnicalName;
                model.AssistantName = row.AssistantName;
                model.DeviceName = row.DeviceName;
                model.MeasurementDate = row.MeasurementDate;
                model.ReceiptDate = row.ReceiptDate;
                model.Note = row.Note;
                model.PatientID = row.PatientIID;
                model.TechnicalID = row.TechnicalID;
                model.TechnicalAssistantID = row.TechnicalAssistantID;
            }

            return View(model);
        }

        // POST: DailyStatues/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dailyStatues = _unitOfWork.DailyStatues.GetById(id);
            _unitOfWork.DailyStatues.Remove(dailyStatues);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.DailyStatues_Delete);

            return RedirectToAction("Index");
        }

        public int? SetRefNO(DailyStatuesModel model)
        {
            if (model.MeasurementDate != null)
                return _unitOfWork.DailyStatues.GetRefNO();
            else
                Note("يجب ادخال تاريخ القياس ");

            model.SetRefNO = false;
            return null;
        }

        public bool CheckData(int id, DailyStatuesModel model = null)
        {
            if (model == null)
            {
                var dailyStatue = _unitOfWork.DailyStatues.GetById(id);

                if (dailyStatue.ReferenceNo != null && dailyStatue.TechnicalID > 0)
                {
                    dailyStatue.Receipt();
                    _unitOfWork.Complete(p => p.DailyStatues_Edit);
                    return true;
                }
                else
                {
                    Note("يجب ادخال كامل البيانات قبل التسليم!");
                    return false;
                }
            }
            else
            {
                var dailyStatue = model;
                var _dailyStatue = _unitOfWork.DailyStatues.GetById(model.DailyStatuesID);

                if (dailyStatue.ReferenceNo != null && dailyStatue.TechnicalID > 0)
                {
                    _dailyStatue.Receipt();
                    _unitOfWork.Complete(p => p.DailyStatues_Edit);
                    return true;
                }
                else
                {
                    Note("يجب ادخال كامل البيانات قبل التسليم!");
                    return false;
                }
            }
        }

        private bool HavePermission(bool permission = true)
  => ViewBag.Permissions.DailyStatues && permission;

        public DailyStatuesModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<DailyStatuesModel>(RequestState.NoPermission);

            return new DailyStatuesModel()
            {
                CanCreate = ViewBag.Permissions.DailyStatues_Create,
                CanEdit = ViewBag.Permissions.DailyStatues_Edit,
                CanDelete = ViewBag.Permissions.DailyStatues_Delete,
                DailyStatuesGrid = _unitOfWork.DailyStatues.GetAll().ToGrid(),
                PatientGrid = _unitOfWork.Patient.GetAll().ToGrid(),
                EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid(),
                DeviceList = _unitOfWork.Device.GetAll().Where(d => d.Type == Types.Daily).ToList(),
            };
        }

        public DailyStatuesModel Refresh(DailyStatuesModel model)
        {
            GetPermission();
            if (!HavePermission())
                return Null<DailyStatuesModel>(RequestState.NoPermission);

            model.CanCreate = ViewBag.Permissions.DailyStatues_Create;
            model.CanEdit = ViewBag.Permissions.DailyStatues_Edit;
            model.CanDelete = ViewBag.Permissions.DailyStatues_Delete;

            //model.DailyStatuesGrid = _unitOfWork.DailyStatues.GetAll().ToGrid();
            model.PatientGrid = _unitOfWork.Patient.GetAll().ToGrid();
            model.EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid();

            model.DeviceList = _unitOfWork.Device.GetAll().Where(d => d.Type == Types.Daily).ToList();

            if (model.PatientID > 0)
                model.PatientName = _unitOfWork.Patient.GetPatientName(model.PatientID);

            if (model.TechnicalID > 0)
                model.TechnicalName = _unitOfWork.Employee.GetById((int)model.TechnicalID).EmployeeName;

            if (model.TechnicalAssistantID > 0)
                model.AssistantName = _unitOfWork.Employee.GetById((int)model.TechnicalAssistantID).EmployeeName;

            if (model.ReferenceNo != null)
                model.SetRefNO = true;


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
