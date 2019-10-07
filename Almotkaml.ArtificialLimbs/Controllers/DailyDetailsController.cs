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
    public class DailyDetailsController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        public DailyDetailsController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        // GET: DailyDetails
        public ActionResult Index()
        {
            return RedirectToAction("Edit",0);
        }
        
        // GET: DailyDetails/Edit/5
        public ActionResult Edit(int id, FormCollection form)
        {
            var select =  form["select"];
           
            if (id > 0 )
                return View(IndexPerpare(id));
          
            return View();
        }

        // POST: DailyDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(DailyDetailsModel model, int id, FormCollection form)
        {
            var select = IntValue( form["select"]);
            var delete = IntValue(form["Delete"]);
            if (select > 0)
                return View(Select(select));


            if (form["save"] != null)
            {
                try
                {
                    Create(model);
                    return RedirectToAction("Edit", id);
                }
                catch
                {
                    return View();
                }
            }
            if (delete > 0)
            {
                try
                {
                    Delete(delete);
                    return RedirectToAction("Edit", id);
                }
                catch
                {
                    return View();
                }
            }
            return View(Select(id));
        }

        


        ////////////////
        public bool Create(DailyDetailsModel model)
        {
            if (model == null)
                return false;

            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyStatues_Edit))
                return false;


            if (!ModelState.IsValid)
                return false;
                var dailyStatues = DailyDetails
                    .New(model.DailyStatuesID, 
                        model.MeasurementModel, 
                        model.Part, 
                        model.AmputationPart, 
                        model.PartMeasure, 
                        model.ShoeNO);
                _unitOfWork.DailyDetails.Add(dailyStatues);
                _unitOfWork.Complete(p => p.DailyStatues_Create);
                SuccessCreateNote();
                
            

            return true;
        }
        public bool Delete(int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DailyStatues_Delete))
                return false;

            if (id < 0)
            {
                return false;
            }
            
            var entity = _unitOfWork.DailyDetails.GetDailyDetails(id);
            if (entity == null)
                return false;

            _unitOfWork.DailyDetails.Remove(entity);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.DailyStatues_Delete);


            return true;
        }

        private bool HavePermission(bool permission = true)
                => ViewBag.Permissions.DailyStatues && permission;
        public DailyDetailsModel IndexPerpare(int id)
        {
            //if (!HavePermission(ApplicationUser.Permissions.Vacation_Edit))
            //    return Fail(RequestState.NoPermission);
            if (id <= 0)
                return null;

            DailyDetailsModel model = new DailyDetailsModel();

            //var daily = _unitOfWork.DailyDetails.GetDailyWithDetails(model.DailyStatuesID);
            var daily = _unitOfWork.DailyDetails.Find(id);
           
            var PatientName = _unitOfWork.Patient.GetPatientName(daily.dailyStatues.PatientID);
            if (daily == null)
                return null;
            return new DailyDetailsModel
            {
                DailyStatuesID = daily.DailyStatuesID,
                PatientID = daily.dailyStatues?.PatientID ?? 0,
                PatientName = PatientName ?? "null",
                DailyDetailsGrid = _unitOfWork.DailyDetails
                                               .GetDailyWithDetails(daily.DailyStatuesID)
                                               .ToGrid(),

                //DailyDetailsID = daily.DailyDetailsID,
                //AmputationPart = daily.AmputationPart,
                //MeasurementModel = daily.MeasurementModel,
                ////Note              = daily.Note,
                //Part = daily.Part,
                //PartMeasure = daily.PartMeasure,
                //ShoeNO = daily.shoesNo,
            };   
        }
        public DailyDetailsModel Select(int id)
        {
            //if (!HavePermission(ApplicationUser.Permissions.Vacation_Edit))
            //    return Fail(RequestState.NoPermission);
            if (id <= 0)
                return null;

            DailyDetailsModel model = new DailyDetailsModel();

            //var daily = _unitOfWork.DailyDetails.GetDailyWithDetails(model.DailyStatuesID);
            var daily = _unitOfWork.DailyDetails.GetDailyDetails(id);

            var PatientName = _unitOfWork.Patient.GetPatientName(daily.dailyStatues.PatientID);
            if (daily == null)
                return null;
            return new DailyDetailsModel
            {
                DailyStatuesID = daily.DailyStatuesID,
                PatientID = daily.dailyStatues?.PatientID ?? 0,
                PatientName = PatientName ?? "null",
                DailyDetailsGrid = _unitOfWork.DailyDetails
                                               .GetDailyWithDetails(daily.DailyStatuesID)
                                               .ToGrid(),
                DailyDetailsID = daily.DailyDetailsID,
                AmputationPart = daily.AmputationPart,
                MeasurementModel = daily.MeasurementModel,
                //Note              = daily.Note,
                Part = daily.Part,
                PartMeasure = daily.PartMeasure,
                ShoeNO = daily.shoesNo,
            };
        }
    }
}
