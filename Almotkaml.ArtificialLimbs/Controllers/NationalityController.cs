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
    public class NationalityController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public NationalityController()
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
            if (!HavePermission(ViewBag.Permissions.Nationality_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NationalityModel model)
        {
            if (ModelState.IsValid)
            {
                var nationality = Nationality.New(model.Name);
                _unitOfWork.Nationality.Add(nationality);
                SuccessCreateNote();
                _unitOfWork.Complete(p => p.Nationality_Create);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(NationalityModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Nationality_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nationality = _unitOfWork.Nationality.GetAll().Where(s => s.NationalityId == id).ToGrid();

            if (nationality == null)
            {
                return HttpNotFound();
            }
            foreach (var row in nationality)
            {
                model.Name = row.Name;
                model.NationalityId = row.NationalityId;
            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NationalityModel model)
        {
            if (ModelState.IsValid)
            {
                var nationality = _unitOfWork.Nationality.GetById(model.NationalityId);
                nationality.Modify(model.Name);
                _unitOfWork.Complete(p => p.Nationality_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(NationalityModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Nationality_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nationality = _unitOfWork.Nationality.GetAll().Where(s => s.NationalityId == id).ToGrid();

            if (nationality == null)
            {
                return HttpNotFound();
            }
            foreach (var row in nationality)
            {
                model.Name = row.Name;
                model.NationalityId = row.NationalityId;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nationality = _unitOfWork.Nationality.GetById(id);
            _unitOfWork.Nationality.Remove(nationality);
            _unitOfWork.Complete(p => p.Nationality_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }
        private bool HavePermission(bool permission = true)
           => ViewBag.Permissions.Nationality && permission;

        public NationalityModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<NationalityModel>(RequestState.NoPermission);

            return new NationalityModel()
            {
                CanCreate = ViewBag.Permissions.Nationality_Create,
                CanEdit = ViewBag.Permissions.Nationality_Edit,
                CanDelete = ViewBag.Permissions.Nationality_Delete,
                NationalityGrid = _unitOfWork.Nationality.GetAll().ToGrid()

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