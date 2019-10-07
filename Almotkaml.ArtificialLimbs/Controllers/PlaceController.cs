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
    public class PlaceController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public PlaceController()
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
            if (!HavePermission(ViewBag.Permissions.Place_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            PlaceModel model = new PlaceModel();
            model.BranchList = _unitOfWork.Branch.GetAll().ToList();
            return View(model);
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                var place = Place.New(model.Name, model.BranchId);
                _unitOfWork.Place.Add(place);
                _unitOfWork.Complete(p => p.Place_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(PlaceModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Place_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var place = _unitOfWork.Place.GetAll().Where(s => s.PlaceId == id).ToGrid();
            // Prepare();
            if (place == null)
            {
                return HttpNotFound();
            }
            model.BranchList = _unitOfWork.Branch.GetAll().ToList();
            foreach (var row in place)
            {
                model.Name = row.Name;
                model.PlaceId = row.PlaceId;
                model.BranchId = row.BranchId;

            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                var place = _unitOfWork.Place.GetById(model.PlaceId);
                place.Modify(model.Name, model.BranchId);
                _unitOfWork.Complete(p => p.Place_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(PlaceModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Place_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var place = _unitOfWork.Place.GetAll().Where(s => s.PlaceId == id).ToList();

            if (place == null)
            {
                return HttpNotFound();
            }
            foreach (var row in place)
            {
                model.Name = row.Name;
                model.PlaceId = row.PlaceId;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var Place = _unitOfWork.Place.GetById(id);
            _unitOfWork.Place.Remove(Place);
            _unitOfWork.Complete(p => p.Place_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
         => ViewBag.Permissions.Place && permission;

        public PlaceModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<PlaceModel>(RequestState.NoPermission);

            return new PlaceModel()
            {
                CanCreate = ViewBag.Permissions.Place_Create,
                CanEdit = ViewBag.Permissions.Place_Edit,
                CanDelete = ViewBag.Permissions.Place_Delete,
                PlaceGrid = _unitOfWork.Place.GetAll().ToGrid(),
                BranchList = _unitOfWork.Branch.GetAll().ToList(),

            };
        }

        public void Refresh(PlaceModel model)
        {

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