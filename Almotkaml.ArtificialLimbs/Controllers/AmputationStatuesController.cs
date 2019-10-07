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
    public class AmputationStatuesController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public AmputationStatuesController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: AmputationStatues
        public ActionResult Index()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View(model);
        }

        // POST : AmputationStatues
        [HttpPost]
        public ActionResult Index(FormCollection form, AmputationStatuesModel model)
        {
            var recipt = IntValue(form["Receipt"]);
            var searchVal = IntValue(form["Searchbtn"]);
            var state = form["State"];
            
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
                model.AmputationStatuesGrid = _unitOfWork.AmputationStatues.Search(model, state).ToGrid();
                //return View(Refresh(model));
            }
            return View(Refresh(model));
        }

        // GET: AmputationStatues/Create
        public ActionResult Create(int deviceID = 0, int Id = 0)
        {
            if (Id <= 0)
            {
                var models = Prepare();
                return View(models);
            }
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationStatues_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            var model = Prepare();
            model.PatientID = Id;
            model.DeviceID = deviceID;
            return View(Refresh(model));

        }

        // POST: AmputationStatues/Create
        [HttpPost]
        public ActionResult Create(AmputationStatuesModel model , FormCollection form)
        {
            if (model.SetRefNO && model.ReferenceNo == null)
                model.ReferenceNo = SetRefNO(model);

            var value = form["Save"];

            if (value != null)
            {
                var device = _unitOfWork.Device.GetById(model.DeviceID);//.Where(s => s.DeviceID == id);
                if (_unitOfWork.AmputationReason.AmputationReasonExist(model.AmputationReason))
                {
                    model.AmputationReasonID = _unitOfWork.AmputationReason.Find(model.AmputationReason);
                }
                else
                    model.AmputationReasonID = _unitOfWork.AmputationReason.AddNew(model.AmputationReason);


                if (device != null && model.PatientID != 0)
                {
                    if (ModelState.IsValid)
                    {
                        var amputationStatues = AmputationStatues.New(model.PatientID, model.TechnicalID, model.TechnicalAssistantID, model.DeviceID, model.MeasurementDate, model.FirstTestDate, model.SecondTestDate, model.ReceiptDate, model.ReferenceNo, model.AmputationTypeID, model.AmputationPart, model.AmputationReasonID, model.AmputationDate, model.Note);
                        _unitOfWork.AmputationStatues.Add(amputationStatues);
                        _unitOfWork.Complete(p => p.AmputationStatues_Create);
                        SuccessCreateNote();
                        return RedirectToAction("Index");
                    }

                    return View(Refresh(model));
                }
            }

            return View(Refresh(model));

        }
        public int? SetRefNO(AmputationStatuesModel  model)
        {
            if (model.MeasurementDate != null)
                return _unitOfWork.AmputationStatues.GetRefNO();
            else
                Note("يجب ادخال تاريخ القياس ");

            model.SetRefNO = false;
            return null;
        }
        public ActionResult NewStatues()
        {
            var model = Prepare();
            model.DeviceList = _unitOfWork.Device.GetAll().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult NewStatues(AmputationStatuesModel model)
        {

            var device = _unitOfWork.Device.GetById(model.DeviceID);//.Where(s => s.DeviceID == id);


            if (device != null && model.PatientID != 0)
            {
                    int StatuesId = _unitOfWork.Patient.AddNewStatues(model.PatientID, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(AmputationStatuesController.Edit), ControllerName(nameof(AmputationStatuesController)), routeValues: new { id = StatuesId });
            }
            return View(Refresh(model));
        }


        // GET: AmputationStatues/Edit/5
        public ActionResult Edit(int id, AmputationStatuesModel model)
        {
            model = Refresh(model);

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationStatues_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var amputationStatues = _unitOfWork.AmputationStatues.GetAll().Where(s => s.AmputationStatuesID == id).ToGrid();

            if (amputationStatues == null)
            {
                return HttpNotFound();
            }
            foreach (var row in amputationStatues)
            {
                model.AmputationStatuesID = row.AmputationStatuesID;
                model.PatientID = row.PatientID;
                model.PatientName = row.PatientName;
                model.TechnicalID = row.TechnicalID;
                model.TechnicalName = row.TechnicalName;
                model.AssistantName = row.AssistantName;
                model.DeviceID = row.DeviceID;
                model.DeviceName = row.DeviceName;
                model.MeasurementDate = row.MeasurementDate;
                model.FirstTestDate = row.FirstTestDate;
                model.SecondTestDate = row.SecondTestDate;
                model.ReceiptDate = row.ReceiptDate;
                model.ReferenceNo = row.ReferenceNo;
                model.Note = row.Note;
                model.PatientID = row.PatientID;
                model.TechnicalID = row.TechnicalID;
                model.TechnicalAssistantID = row.TechnicalAssistantID;
                model.AmputationTypeID = row.AmputationTypeID;
                model.AmputationPart = row.AmputationPart;
                model.AmputationReason = row.AmputationReason;
                model.AmputationDate = row.AmputationDate;
            }

            return View(Refresh(model));
        }

        // POST: AmputationStatues/Edit/5
        [HttpPost]
        public ActionResult Edit( AmputationStatuesModel model, FormCollection form)
        {
            var value = form["Save"];

            if (model.SetRefNO && model.ReferenceNo == null)
                model.ReferenceNo = SetRefNO(model);

            if (value != null)
            {
                if (_unitOfWork.AmputationReason.AmputationReasonExist(model.AmputationReason))
                {
                    model.AmputationReasonID = _unitOfWork.AmputationReason.Find(model.AmputationReason);
                }
                else
                    model.AmputationReasonID = _unitOfWork.AmputationReason.AddNew(model.AmputationReason);

                if (ModelState.IsValid)
                {
                    var amputationStatues = _unitOfWork.AmputationStatues.GetById(model.AmputationStatuesID);
                    amputationStatues.Modify(model.PatientID, model.TechnicalID, model.TechnicalAssistantID, model.DeviceID, model.MeasurementDate, model.FirstTestDate, model.SecondTestDate, model.ReceiptDate, model.ReferenceNo, model.AmputationTypeID, model.AmputationPart, model.AmputationReasonID, model.AmputationDate, model.Note);
                    SuccessEditNote();
                    _unitOfWork.Complete(p => p.AmputationStatues_Edit);
                    return RedirectToAction("Index");
                }
            }
            return View(Refresh(model));
        }

        // GET: AmputationStatues/Delete/5
        public ActionResult Delete(int id,AmputationStatuesModel model)
        {
            model = Refresh(model);
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationStatues_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var amputationStatues = _unitOfWork.AmputationStatues.GetAll().Where(s => s.AmputationStatuesID == id).ToGrid();

            if (amputationStatues == null)
            {
                return HttpNotFound();
            }
            foreach (var row in amputationStatues)
            {
                model.AmputationStatuesID = row.AmputationStatuesID;
                model.PatientID = row.PatientID;
                model.PatientName = row.PatientName;
                model.TechnicalID = row.TechnicalID;
                model.TechnicalName = row.TechnicalName;
                model.AssistantName = row.AssistantName;
                model.DeviceID = row.DeviceID;
                model.DeviceName = row.DeviceName;
                model.MeasurementDate = row.MeasurementDate;
                model.FirstTestDate = row.FirstTestDate;
                model.SecondTestDate = row.SecondTestDate;
                model.ReceiptDate = row.ReceiptDate;
                model.ReferenceNo = row.ReferenceNo;
                model.Note = row.Note;
                model.PatientID = row.PatientID;
                model.TechnicalID = row.TechnicalID;
                model.TechnicalAssistantID = row.TechnicalAssistantID;
                model.AmputationTypeID = row.AmputationTypeID;
                model.AmputationPart = row.AmputationPart;
                model.AmputationReason = row.AmputationReason;
                model.AmputationDate = row.AmputationDate;
            }

            return View(model);
        }

        // POST: AmputationStatues/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var amputationStatues = _unitOfWork.AmputationStatues.GetById(id);
            _unitOfWork.AmputationStatues.Remove(amputationStatues);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.AmputationStatues_Delete);

            return RedirectToAction("Index");
        }

        public bool CheckData(int id, AmputationStatuesModel model = null)
        {
            if (model == null)
            {
                var amputationStatues = _unitOfWork.AmputationStatues.GetById(id);

                if (amputationStatues.ReferenceNo != null && amputationStatues.TechnicalID > 0)
                {
                    amputationStatues.Receipt();
                    _unitOfWork.Complete(p => p.AmputationStatues_Edit);
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
                var amputationStatues = model;
                var _amputationStatues = _unitOfWork.AmputationStatues.GetById(model.AmputationStatuesID);

                if (amputationStatues.ReferenceNo != null && amputationStatues.TechnicalID > 0)
                {
                    _amputationStatues.Receipt();
                    _unitOfWork.Complete(p => p.AmputationStatues_Edit);
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
  => ViewBag.Permissions.AmputationStatues && permission;

        public AmputationStatuesModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<AmputationStatuesModel>(RequestState.NoPermission);

            return new AmputationStatuesModel()
            {
                CanCreate = ViewBag.Permissions.AmputationStatues_Create,
                CanEdit = ViewBag.Permissions.AmputationStatues_Edit,
                CanDelete = ViewBag.Permissions.AmputationStatues_Delete,
                AmputationStatuesGrid =_unitOfWork.AmputationStatues.GetAll().ToGrid(),
                PatientGrid = _unitOfWork.Patient.GetAll().ToGrid(),
                EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid(),
                DeviceList = _unitOfWork.Device.GetAll().ToList(),
                AmputationTypesList=_unitOfWork.AmputationTypes.GetAll().ToList(),
                AmputationReasonList = _unitOfWork.AmputationReason.GetAll().ToList(),
            };
        }

        public AmputationStatuesModel Refresh(AmputationStatuesModel model)
        {
            GetPermission();
            if (!HavePermission())
                return Null<AmputationStatuesModel>(RequestState.NoPermission);

            model.CanCreate = ViewBag.Permissions.AmputationStatues_Create;
            model.CanEdit = ViewBag.Permissions.AmputationStatues_Edit;
            model.CanDelete = ViewBag.Permissions.AmputationStatues_Delete;

            //model.AmputationStatuesGrid  = _unitOfWork._AmputationStatues.GetAll().ToGrid();
            model.PatientGrid = _unitOfWork.Patient.GetAll().ToGrid();
            model.EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid();
            model.AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList();
            model.AmputationReasonList = _unitOfWork.AmputationReason.GetAll().ToList();
            model.DeviceList = _unitOfWork.Device.GetAll().Where (d=>d.Type == Types.Amputation).ToList();

            if (model.PatientID > 0)
                model.PatientName = _unitOfWork.Patient.GetById(model.PatientID).PatientName;

            if (model.DeviceID > 0)
                model.DeviceName = _unitOfWork.Device.GetById(model.DeviceID).ArabicName;

            if (model.TechnicalID > 0)
                model.TechnicalName = _unitOfWork.Employee.GetById((int)model.TechnicalID).EmployeeName;

            if (model.TechnicalAssistantID > 0)
                model.AssistantName = _unitOfWork.Employee.GetById((int)model.TechnicalAssistantID).EmployeeName;

            if (model.AmputationTypeID > 0)
                model.AmputationTypeID = _unitOfWork.AmputationTypes.GetById(model.AmputationTypeID).AmputationTypeID;

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
