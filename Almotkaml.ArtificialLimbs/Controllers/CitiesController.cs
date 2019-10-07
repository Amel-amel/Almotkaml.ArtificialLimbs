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
using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public CitiesController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Countries
        public ActionResult Index()
        {
            var models = Prepare();
           if(models == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View(models);
        }


        // GET: Countries/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Cities_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            CityModel model = new CityModel();
            model.CountryList = _unitOfWork.Country.GetAll().ToList();
            return View(model);
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityModel model)
        {
            if (ModelState.IsValid)
            {
                var city = City.New(model.Name, model.CountryId);
                _unitOfWork.City.Add(city);
                _unitOfWork.Complete(p => p.Cities_Create);
                SuccessCreateNote(); 
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(CityModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Cities_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var City = _unitOfWork.City.GetAll().Where(s => s.CityId == id).ToGrid();
           // Prepare();
            if (City == null)
            {
                return HttpNotFound();
            }
            model.CountryList = _unitOfWork.Country.GetAll().ToList();
            foreach (var row in City)
            {
                model.Name = row.Name;
                model.CityId = row.CityId;
                model.CountryId = row.CountryId;
            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityModel model)
        {
            if (ModelState.IsValid)
            {
                var City = _unitOfWork.City.GetById(model.CityId);
                City.Modify(model.Name,model.CountryId);
                _unitOfWork.Complete(p => p.Cities_Edit);
                SuccessEditNote();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(CityModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Cities_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var City = _unitOfWork.City.GetAll().Where(s => s.CityId == id).ToGrid();

            if (City == null)
            {
                return HttpNotFound();
            }
            foreach (var row in City)
            {
                model.Name = row.Name;
                model.CityId = row.CityId;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var City = _unitOfWork.City.GetById(id);
            _unitOfWork.City.Remove(City);
            _unitOfWork.Complete(p => p.Cities_Delete);
            SuccessDeleteNote();
            return RedirectToAction("Index");
        }

        
        private bool HavePermission(bool permission = true)
           => ViewBag.Permissions.Cities && permission;

        public CityModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<CityModel>(RequestState.NoPermission);

            return new CityModel()
            {
                CanCreate = ViewBag.Permissions.Cities_Create,
                CanEdit = ViewBag.Permissions.Cities_Edit,
                CanDelete = ViewBag.Permissions.Cities_Delete,
                CityGrid = _unitOfWork.City.GetAll().ToGrid(),
                CountryList = _unitOfWork.Country.GetAll().ToList(),

            };
        }

        public void Refresh(CityModel model)
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
