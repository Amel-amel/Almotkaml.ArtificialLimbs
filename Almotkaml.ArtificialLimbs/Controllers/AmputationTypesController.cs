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
    public class AmputationTypesController : BaseController
    {
        // GET: AmputationTypes
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public AmputationTypesController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: AmputationTypes
        public ActionResult Index()
        {
            var models = Prepare();
            if (models == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(models);
        }



        // GET: Countries/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationTypes_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AmputationTypesModel model)
        {
            if (ModelState.IsValid)
            {
                var amputationTypes = AmputationTypes.New(model.AmputationName);
                _unitOfWork.AmputationTypes.Add(amputationTypes);
                _unitOfWork.Complete(p => p.AmputationTypes_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(AmputationTypesModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationTypes_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var amputationTypes = _unitOfWork.AmputationTypes.GetAll().Where(s => s.AmputationTypeID == id).ToGrid();

            if (amputationTypes == null)
            {
                return HttpNotFound();
            }
            foreach (var row in amputationTypes)
            {
                model.AmputationName = row.Name;
                model.AmputationTypesID = row.AmputationTypesID;
            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AmputationTypesModel model)
        {
            if (ModelState.IsValid)
            {
                var amputationTypes = _unitOfWork.AmputationTypes.GetById(model.AmputationTypesID);
                amputationTypes.Modify(model.AmputationName);
                _unitOfWork.Complete(p => p.AmputationTypes_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(AmputationTypesModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.AmputationTypes_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var amputationTypes = _unitOfWork.AmputationTypes.GetAll().Where(s => s.AmputationTypeID == id).ToGrid();

            if (amputationTypes == null)
            {
                return HttpNotFound();
            }
            foreach (var row in amputationTypes)
            {
                model.AmputationName = row.Name;
                model.AmputationTypesID = row.AmputationTypesID;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var amputationTypes = _unitOfWork.AmputationTypes.GetById(id);
            _unitOfWork.AmputationTypes.Remove(amputationTypes);
            _unitOfWork.Complete(p => p.AmputationTypes_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
         => ViewBag.Permissions.AmputationTypes && permission;


        public AmputationTypesModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<AmputationTypesModel>(RequestState.NoPermission);

            return new AmputationTypesModel()
            {
                CanCreate = ViewBag.Permissions.AmputationTypes_Create,
                CanEdit = ViewBag.Permissions.AmputationTypes_Edit,
                CanDelete = ViewBag.Permissions.AmputationTypes_Delete,
                AmputationTypesGrid = _unitOfWork.AmputationTypes.GetAll().ToGrid()

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
