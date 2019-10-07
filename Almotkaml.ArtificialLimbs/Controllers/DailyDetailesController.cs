using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using System.Web;
using System.Web.Mvc;
using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class DailyDetailesController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;
        private ArtificialLimbsDbContext db ;

        private bool HavePermission(bool permission = true)
                => ViewBag.Permissions.DailyDetailes && permission;

        public DailyDetailesController()
        {
            db = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(db);
        }
        // GET: DailyDetailes
        public ActionResult Index(DailyDetailsModel model,int id, int PatientIID, FormCollection form)
        {
            model = Perpare(id,PatientIID);
            //var dailyDetails = db.DailyDetails.Include(d => d.dailyStatues);
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        // GET: DailyDetailes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyDetails dailyDetails = db.DailyDetails.Find(id);
            if (dailyDetails == null)
            {
                return HttpNotFound();
            }
            return View(dailyDetails);
        }

        // GET: DailyDetailes/Create
        public ActionResult Create( FormCollection form, int id = 0, int PatientIID = 0)
        {
            //ViewBag.DailyStatuesID = new SelectList(db.DailyStatues, "DailyStatuesID", "MeasurementDate");
            //return View();
            var model = Perpare(id, PatientIID);
            return View(model);
        }

        // POST: DailyDetailes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, int PatientIID, FormCollection form, DailyDetailsModel model)
        {
            if (model == null)
                return HttpNotFound();

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyDetailes_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            if (form["save"] != null)
                if (ModelState.IsValid)
                {
                    var dailyStatues = DailyDetails
                        .New(model.DailyStatuesID,
                            model.MeasurementModel,
                            model.Part,
                            model.Note,
                            model.PartMeasure,
                            model.ShoeNO);
                    _unitOfWork.DailyDetails.Add(dailyStatues);
                    _unitOfWork.Complete(p => p.DailyDetailes_Create);
                    SuccessCreateNote();
                    return RedirectToAction("Index",new { id=id,PatientIID=PatientIID});
                }

            //ViewBag.DailyStatuesID = new SelectList(db.DailyStatues, "DailyStatuesID", "MeasurementDate", dailyDetails.DailyStatuesID);
            return View(model);
        }

        // GET: DailyDetailes/Edit/5
        public ActionResult Edit(DailyDetailsModel model,int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyDetailes_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DailyDetails = _unitOfWork.DailyDetails.GetDailyWithDetails(id);
            // Prepare();
            if (DailyDetails == null)
            {
                return HttpNotFound();
            }
            foreach (var d in DailyDetails)
            {
                model.PatientID = d.dailyStatues.PatientID;
                model.PatientName = d.dailyStatues.patient.PatientName;
                model.DailyDetailsID = d.DailyDetailsID;
                model.DailyStatuesID = d.DailyStatuesID;
                //model.AmputationPart = d.dailyStatues.AmputationPart;
                model.MeasurementModel = d.MeasurementModel;
                model.Note = d.Note;
                model.Part = d.Part;
                model.PartMeasure = d.PartMeasure;
                model.ShoeNO = d.shoesNo;
            }
             return View(model);
        }

        // POST: DailyDetailes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DailyDetailsModel model,FormCollection form)
        {
            if (model == null)
                return HttpNotFound();

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyDetailes_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            model.path = Convert.ToString((int)model.MeasurementModel);
            if (form["save"] != null)
            { 
                if (ModelState.IsValid)
                {
                    var dailyDetails = _unitOfWork.DailyDetails.GetById(model.DailyDetailsID);
                   // City.Modify(model.Name, model.CountryId);

                    dailyDetails
                        .Modify(model.DailyStatuesID,
                            model.MeasurementModel,
                            model.Part,
                            model.Note,
                            model.PartMeasure,
                            model.ShoeNO);
                    //_unitOfWork.DailyDetails.Add(dailyDetails);
                    _unitOfWork.Complete(p => p.DailyDetailes_Edit);
                    SuccessEditNote();
                    return RedirectToAction("Index", new { id = model.DailyStatuesID, PatientIID = model.PatientID });
                }
            }
            return View(model);
        }

        // GET: DailyDetailes/Delete/5
        public ActionResult Delete(DailyDetailsModel model,int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyDetailes_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DailyDetails = _unitOfWork.DailyDetails.GetDailyWithDetails(id);
            
            if (DailyDetails == null)
            {
                return HttpNotFound();
            }
            foreach (var row in DailyDetails)
            {
                model.PatientName = row.dailyStatues?.patient.PatientName;
                model.PatientID = row.dailyStatues.PatientID;
                model.DailyStatuesID = row.DailyStatuesID;
                model.DailyDetailsID = row.DailyDetailsID;
            }
            return View(model);

        }

        // POST: DailyDetailes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyDetailes_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));


            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            var entity = _unitOfWork.DailyDetails.GetDailyDetails(id);

            var DailyStatuesID = entity.DailyStatuesID;
            var PatientID = entity.dailyStatues.PatientID;

            if (entity == null)
                return HttpNotFound();

            _unitOfWork.DailyDetails.Remove(entity);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.DailyDetailes_Delete);
            return RedirectToAction("Index", new { id = DailyStatuesID, PatientIID = PatientID });

        }


        public DailyDetailsModel Perpare(int id, int PatientID)
        {
            GetPermission();
            if (!HavePermission())
                return Null<DailyDetailsModel>(RequestState.NoPermission);

            if (id <= 0)
                return null;

            DailyDetailsModel model = new DailyDetailsModel();

            var daily = _unitOfWork.DailyDetails.Find(id);

            var PatientName = _unitOfWork.Patient.GetPatientName(PatientID);
            if (daily == null && PatientName == null)
                return null;
            return new DailyDetailsModel
            {
                CanCreate = ViewBag.Permissions.DailyDetailes_Create,
                CanEdit = ViewBag.Permissions.DailyDetailes_Edit,
                CanDelete = ViewBag.Permissions.DailyDetailes_Delete,
                DailyStatuesID = id,
                PatientID = PatientID,
                PatientName = PatientName ?? "null",
                DailyDetailsGrid = _unitOfWork.DailyDetails?
                                               .GetDailyWithDetails2(id)
                                               .ToGrid(),
               // AmputationPart = daily.AmputationPart,
            
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
