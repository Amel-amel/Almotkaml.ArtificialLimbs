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
    public class PatientController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public PatientController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Patient
        public ActionResult Index()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View(model);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Patient_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View(Prepare());
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,PatientModel model)
        {
            if (_unitOfWork.Patient.PatientExist(model.NationalityNumber))
            {
                Note("الرقم الوطني الذي أدخلته موجود مسبقا .");
                return View(Refresh (model));
            }

            if (ModelState.IsValid)
            {
                var patient = Patient.New(model.PatientName,model.Gender,model.BirthDate,model.PhoneNumber,model.NationalityNumber,model.NationalityID ,model.CityID,model.PlaceId,model.RegistrationDate,model.Note);
                _unitOfWork.Patient.Add(patient);
                _unitOfWork.Complete(p => p.Patient_Create);
                SuccessCreateNote();
                return RedirectToAction("NewStatues",routeValues :new {id= patient.PatientID});
            }

            return View(Refresh(model));
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id,PatientModel model)
        {
            model = Refresh(model);
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Patient_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = _unitOfWork.Patient.GetAll().Where(s => s.PatientID == id).ToGrid();

            if (patient == null)
            {
                return HttpNotFound();
            }
            foreach (var row in patient)
            {
                model.PatientID = row.PatientID;
                model.PatientName = row.PatientName;
                model.PhoneNumber  = row.PhoneNumber ;
                model.NationalityNumber = row.NationalityNumber;
                model.NationalityID = row.NationalityID;
                model.CityID = row.CityID;
                model.PlaceId  = row.PlaceId;
                model.Note = row.Note;
                model.BirthDate = row.BirthDate;
                model.RegistrationDate = row.RegistrationDate;
                model.Gender = row.Gender;
            }

            return View(model);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit( PatientModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = _unitOfWork.Patient.GetById(model.PatientID);
                patient.Modify(model.PatientName, model.Gender, model.BirthDate, model.PhoneNumber, model.NationalityNumber, model.NationalityID, model.CityID, model.PlaceId, model.RegistrationDate, model.Note);
                SuccessEditNote();
                _unitOfWork.Complete(p => p.Patient_Edit);
                return RedirectToAction("Index");
            }
            return View(Refresh(model));
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id,PatientModel model)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Patient_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = _unitOfWork.Patient.GetAll().Where(s => s.PatientID == id).ToGrid();

            if (patient == null)
            {
                return HttpNotFound();
            }
            foreach (var row in patient)
            {
                model.PatientID = row.PatientID;
                model.PatientName = row.PatientName;
                model.PhoneNumber = row.PhoneNumber;
                model.NationalityNumber = row.NationalityNumber;
                model.NationalityID = row.NationalityID;
                model.CityID = row.CityID;
                model.PlaceId = row.PlaceId;
                model.Note = row.Note;
                model.BirthDate = row.BirthDate;
                model.RegistrationDate = row.RegistrationDate;
                model.Gender = row.Gender;
            }

            return View(model);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var patient = _unitOfWork.Patient.GetById(id);
            _unitOfWork.Patient.Remove(patient);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.Patient_Delete);

            return RedirectToAction("Index");
        }

        //Get : تسجيل حالة جديدة
        public ActionResult NewStatues(int id = 0)
        {
            var model = Prepare();
            model.PatientID = id;
            // model.DeviceList = _unitOfWork.Device.GetAll().ToList();
            return View(Refresh(model));
        }

        [HttpPost]
        public ActionResult NewStatues(PatientModel model, int id = 0)
        {
            var device = _unitOfWork.Device.GetById(model.DeviceId);//.Where(s => s.DeviceID == id);

            if (device != null && id != 0)
            {

                if (device.Type == Types.Daily)
                {
                    int StatuesId = _unitOfWork.Patient.AddNewStatues(id, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(DailyStatuesController.Edit), ControllerName(nameof(DailyStatuesController)), routeValues: new { id = StatuesId });
                }
                else
                {
                    //int StatuesId = _unitOfWork.Patient.AddNewStatues(id, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(AmputationStatuesController.Create), ControllerName(nameof(AmputationStatuesController)), routeValues: new { id = model.PatientID });
                }
            }
            if (device != null && model.PatientID != 0)
            {

                if (device.Type == Types.Daily)
                {
                    int StatuesId = _unitOfWork.Patient.AddNewStatues(model.PatientID, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(DailyStatuesController.Edit), ControllerName(nameof(DailyStatuesController)), routeValues: new { id = StatuesId });
                }
                else
                {
                    // int StatuesId = _unitOfWork.Patient.AddNewStatues(model.PatientID, device.DeviceID, device.Type);
                    return RedirectToAction(nameof(AmputationStatuesController.Create), ControllerName(nameof(AmputationStatuesController)), routeValues: new { id = model.PatientID });
                }
            }
            return View(Refresh(model));
        }

        private bool HavePermission(bool permission = true)
     => ViewBag.Permissions.Patient && permission;

        public PatientModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<PatientModel>(RequestState.NoPermission);

            return new PatientModel()
            {
                CanCreate = ViewBag.Permissions.Patient_Create,
                CanEdit = ViewBag.Permissions.Patient_Edit,
                CanDelete = ViewBag.Permissions.Patient_Delete,
                PatientGrid = _unitOfWork.Patient.GetAll().ToGrid(),
                CityList = _unitOfWork.City.GetAll().ToList(),
                NationalityList = _unitOfWork.Nationality.GetAll().ToList(),
                PlaceList = _unitOfWork.Place .GetAll().ToList(),
            };
        }

        public PatientModel Refresh(PatientModel model)
        {
            GetPermission();
            if (!HavePermission())
                return Null<PatientModel>(RequestState.NoPermission);

            model.CanCreate = ViewBag.Permissions.Patient_Create;
            model.CanEdit = ViewBag.Permissions.Patient_Edit;
            model.CanDelete = ViewBag.Permissions.Patient_Delete;

            model.CityList = _unitOfWork.City.GetAll().ToList();
            model.NationalityList = _unitOfWork.Nationality.GetAll().ToList();
            model.PlaceList = _unitOfWork.Place.GetAll().ToList();
            model.DeviceList = _unitOfWork.Device.GetAll().ToList();
            model.PatientGrid = _unitOfWork.Patient.GetAll().ToGrid();

            if (model.PatientID > 0)
                model.PatientName = _unitOfWork.Patient.GetPatientName(model.PatientID);

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
