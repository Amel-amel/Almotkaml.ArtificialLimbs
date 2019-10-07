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
    public class CountriesController : BaseController
    {
        
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public CountriesController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Countries
        public ActionResult Index()
        {
            var models =  Prepare();
            if(models == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(models);
        }



        // GET: Countries/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Countries_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryModel model)
        {
            
            if (ModelState.IsValid)
            {
                var country = Country.New(model.Name);
                _unitOfWork.Country.Add(country);
                _unitOfWork.Complete(p => p.Countries_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(CountryModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Countries_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = _unitOfWork.Country.GetAll().Where(s => s.CountryId == id).ToGrid();

            if (country == null)
            {
                return HttpNotFound();
            }
            foreach (var row in country)
            {
                model.Name = row.Name;
                model.CountryId = row.CountryId;
            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountryModel model)
       {
            if (ModelState.IsValid)
            {
                var country = _unitOfWork.Country.GetById(model.CountryId);
                country.Modify(model.Name);
                _unitOfWork.Complete(p => p.Countries_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(CountryModel model,int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Countries_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = _unitOfWork.Country.GetAll().Where(s => s.CountryId == id).ToGrid();

            if (country == null)
            {
                return HttpNotFound();
            }
            foreach (var row in country)
            {
                model.Name = row.Name;
                model.CountryId = row.CountryId;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var country = _unitOfWork.Country.GetById(id);
            _unitOfWork.Country.Remove(country);
            _unitOfWork.Complete(p => p.Countries_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
         => ViewBag.Permissions.Countries && permission;

        public CountryModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<CountryModel>(RequestState.NoPermission);

            return new CountryModel()
            {
                CanCreate = ViewBag.Permissions.Countries_Create,
                CanEdit = ViewBag.Permissions.Countries_Edit,
                CanDelete = ViewBag.Permissions.Countries_Delete,
                CountryGrid = _unitOfWork.Country.GetAll().ToGrid()

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
