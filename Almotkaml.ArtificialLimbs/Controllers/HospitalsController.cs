using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.ArtificialLimbs.Core.Extensions;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class HospitalsController : BaseController
    {
        // GET: Hospitals
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public HospitalsController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Countries
        public ActionResult Index()
        {
            var models = Prepare();
            if(models==null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(models);
        }



        // GET: Countries/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Hospitals_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HospitalsModel model)
        {
            if (ModelState.IsValid)
            {
                var hospitals = Hospitals.New(model.Name);
                _unitOfWork.Hospitals.Add(hospitals);
                _unitOfWork.Complete(p => p.Hospitals_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(HospitalsModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Hospitals_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hospitals = _unitOfWork.Hospitals.GetAll().Where(s => s.HospitalsId == id).ToGrid();

            if (hospitals == null)
            {
                return HttpNotFound();
            }
            foreach (var row in hospitals)
            {
                model.Name = row.Name;
                model.HospitalsId = row.HospitalsId;
            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HospitalsModel model)
        {
            if (ModelState.IsValid)
            {
                var hospitals = _unitOfWork.Hospitals.GetById(model.HospitalsId);
                hospitals.Modify(model.Name);
                _unitOfWork.Complete(p => p.Hospitals_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(HospitalsModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Hospitals_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hospitals = _unitOfWork.Hospitals.GetAll().Where(s => s.HospitalsId == id).ToGrid();

            if (hospitals == null)
            {
                return HttpNotFound();
            }
            foreach (var row in hospitals)
            {
                model.Name = row.Name;
                model.HospitalsId = row.HospitalsId;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hospitals = _unitOfWork.Hospitals.GetById(id);
            _unitOfWork.Hospitals.Remove(hospitals);
            _unitOfWork.Complete(p => p.Hospitals_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
         => ViewBag.Permissions.Hospitals && permission;


        public HospitalsModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<HospitalsModel>(RequestState.NoPermission);

            return new HospitalsModel()
            {
                CanCreate = ViewBag.Permissions.Hospitals_Create,
                CanEdit = ViewBag.Permissions.Hospitals_Edit,
                CanDelete = ViewBag.Permissions.Hospitals_Delete,
                HospitalsGrid = _unitOfWork.Hospitals.GetAll().ToGrid()

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