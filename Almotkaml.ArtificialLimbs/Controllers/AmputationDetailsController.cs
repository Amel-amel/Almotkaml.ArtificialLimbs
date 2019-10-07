using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class AmputationDetailsController : BaseController
    {
        private readonly ArtificialLimbsDbContext db;
        private readonly UnitOfWork _unitOfWork;
        public AmputationDetailsController()
        {
            db = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(db);
        }
        public ActionResult Index(AmputationDetailsModel model, int id, int PatientIID, FormCollection form)
        {
            model = Perpare(id, PatientIID);
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
        public ActionResult Create(FormCollection form, int id = 0, int PatientIID = 0)
        {
            //ViewBag.AmputationStatuesID = new SelectList(db.DailyStatues, "AmputationStatuesID", "MeasurementDate");
            //return View();
            var model = Perpare(id, PatientIID);
            return View(model);
        }

        // POST: DailyDetailes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, int PatientIID, FormCollection form, AmputationDetailsModel model)
        {
            if (model == null)
                return HttpNotFound();

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationDetails_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            model.path = Convert.ToString((int)model.MeasurementModel);

            if (form["save"] != null)
            {
                if (ModelState.IsValid)
                {
                    var dailyStatues = AmputationDetails
                        .New(model.AmputationStatuesID,
                            model.MeasurementModel,
                            model.Part,
                            model.AmputationPart,
                            model.PartMeasure,
                            model.ShoeNO,
                            model.Note);
                    _unitOfWork.AmputationDetails.Add(dailyStatues);
                    _unitOfWork.Complete(p => p.AmputationDetails_Create);
                    SuccessCreateNote();
                    return RedirectToAction("Index", new { id = id, PatientIID = PatientIID });
                }
            }


            //ViewBag.AmputationStatuesID = new SelectList(db.DailyStatues, "AmputationStatuesID", "MeasurementDate", dailyDetails.AmputationStatuesID);
            return View(model);
        }

        // GET: DailyDetailes/Edit/5
        public ActionResult Edit(AmputationDetailsModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationDetails_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DailyDetails = _unitOfWork.AmputationDetails.GetAmputationWithDetails2(id);
            // Prepare();
            if (DailyDetails == null)
            {
                return HttpNotFound();
            }
            foreach (var d in DailyDetails)
            {
                model.PatientID = d.amputationStatues.PatientID;
                model.PatientName = d.amputationStatues.patient.PatientName;
                model.AmputationDetailsID = d.AmputationDetailsID;
                model.AmputationStatuesID = d.AmputationStatuesID;
                model.AmputationPart = d.amputationStatues.AmputationPart;
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
        public ActionResult Edit(AmputationDetailsModel model, FormCollection form)
        {
            if (model == null)
                return HttpNotFound();

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationDetails_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            model.path = Convert.ToString((int)model.MeasurementModel);

            if (form["save"] != null)
            {
                if (ModelState.IsValid)
                {
                    var amputationDetails = _unitOfWork.AmputationDetails.GetById(model.AmputationDetailsID);
                    // City.Modify(model.Name, model.CountryId);

                    amputationDetails
                        .Modify(model.AmputationStatuesID,
                            model.MeasurementModel,
                            model.Part,
                            model.AmputationPart,
                            model.PartMeasure,
                            model.ShoeNO,
                            model.Note);
                    //_unitOfWork.DailyDetails.Add(dailyDetails);
                    _unitOfWork.Complete(p => p.AmputationDetails_Edit);
                    SuccessEditNote();
                    return RedirectToAction("Index", new { id = model.AmputationStatuesID, PatientIID = model.PatientID });
                }
            }

            return View(model);
        }

        // GET: DailyDetailes/Delete/5
        public ActionResult Delete(AmputationDetailsModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationDetails_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DailyDetails = _unitOfWork.AmputationDetails.GetAmputationWithDetails2(id);

            if (DailyDetails == null)
            {
                return HttpNotFound();
            }
            foreach (var row in DailyDetails)
            {
                model.PatientName = row.amputationStatues?.patient?.PatientName;
                model.PatientID = row.amputationStatues.PatientID;
                model.AmputationStatuesID = row.AmputationStatuesID;
                model.AmputationDetailsID = row.AmputationDetailsID;
            }
            return View(model);

        }

        // POST: DailyDetailes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationDetails_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));


            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _unitOfWork.AmputationDetails.GetAmputationDetails(id);

            var AmputationStatuesID = entity.AmputationStatuesID;
            var PatientID = entity.amputationStatues.PatientID;

            if (entity == null)
                return HttpNotFound();

            _unitOfWork.AmputationDetails.Remove(entity);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.AmputationDetails_Delete);
            return RedirectToAction("Index", new { id = AmputationStatuesID, PatientIID = PatientID });

        }

        private bool HavePermission(bool permission = true)
                => ViewBag.Permissions.AmputationDetails && permission;
        public AmputationDetailsModel Perpare(int AmputationStatuesID, int PatientID)
        {
            GetPermission();
            if (!HavePermission())
                return Null<AmputationDetailsModel>(RequestState.NoPermission);

            if (AmputationStatuesID <= 0)
                return null;
            if (PatientID <= 0)
                return null;

            var daily = _unitOfWork.AmputationDetails.Find(AmputationStatuesID);

            var PatientName = _unitOfWork.Patient.GetPatientName(PatientID);
            var _AmputationPart = _unitOfWork.AmputationStatues.GetById(AmputationStatuesID).AmputationPart;

            if (daily == null && PatientName == null)
                return null;
            return new AmputationDetailsModel
            {
                CanCreate = ViewBag.Permissions.AmputationDetails_Create,
                CanEdit = ViewBag.Permissions.AmputationDetails_Edit,
                CanDelete = ViewBag.Permissions.AmputationDetails_Delete,
                AmputationStatuesID = AmputationStatuesID,
                PatientID = PatientID,
                PatientName = PatientName ?? "null",
                AmputationDetailsGrid = _unitOfWork.AmputationDetails.GetAmputationWithDetails(AmputationStatuesID).ToGrid(),
                AmputationPart = _AmputationPart,
            };
        }
        public AmputationDetailsModel Select(int AmputationStatuesID, int PatientID)
        {
            if (AmputationStatuesID <= 0)
                return null;
            var daily = _unitOfWork.AmputationDetails.GetAmputationDetails(AmputationStatuesID);

            var PatientName = _unitOfWork.Patient.GetPatientName(daily.amputationStatues.PatientID);
            if (daily == null)
                return null;
            return new AmputationDetailsModel
            {
                AmputationStatuesID = daily.AmputationStatuesID,
                PatientID = daily.amputationStatues?.PatientID ?? 0,
                PatientName = PatientName ?? "null",
                AmputationDetailsGrid = _unitOfWork.AmputationDetails
                                               .GetAmputationWithDetails(daily.AmputationStatuesID)
                                               .ToGrid(),
                AmputationDetailsID = daily.AmputationDetailsID,
                AmputationPart = daily.amputationStatues.AmputationPart,
                MeasurementModel = daily.MeasurementModel,
                Note = daily.Note,
                Part = daily.Part,
                PartMeasure = daily.PartMeasure,
                ShoeNO = daily.shoesNo,
            };
        }

    }
}