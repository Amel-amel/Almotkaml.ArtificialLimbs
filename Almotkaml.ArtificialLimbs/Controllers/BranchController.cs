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
    public class BranchController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public BranchController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Countries
        public ActionResult Index()
        {
            var models = Prepare();
            if (models== null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(models);
        }



        // GET: Countries/Create
        public ActionResult Create()
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Branch_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                var branch = Branch.New(model.BranchName);
                _unitOfWork.Branch.Add(branch);
                _unitOfWork.Complete(p => p.Branch_Create);
                SuccessCreateNote();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(BranchModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Branch_Edit))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Branch = _unitOfWork.Branch.GetAll().Where(s => s.BranchId == id).ToGrid();

            if (Branch == null)
            {
                return HttpNotFound();
            }
            foreach (var row in Branch)
            {
                model.BranchName = row.Name;
                model.BranchId = row.BranchId;
            }

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                var branch = _unitOfWork.Branch.GetById(model.BranchId);
                branch.Modify(model.BranchName);
                SuccessEditNote();
                _unitOfWork.Complete(p => p.Branch_Edit);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(BranchModel model, int id)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.Branch_Delete))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var branch = _unitOfWork.Branch.GetAll().Where(s => s.BranchId == id).ToGrid();

            if (branch == null)
            {
                return HttpNotFound();
            }
            foreach (var row in branch)
            {
                model.BranchName = row.Name;
                model.BranchId = row.BranchId;
            }
            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var branch = _unitOfWork.Branch.GetById(id);
            _unitOfWork.Branch.Remove(branch);
            SuccessDeleteNote();
            _unitOfWork.Complete(p => p.Branch_Delete);

            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
           => ViewBag.Permissions.Branch && permission;

        public BranchModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<BranchModel>(RequestState.NoPermission);

            return new BranchModel()
            {
                CanCreate = ViewBag.Permissions.Branch_Create,
                CanEdit = ViewBag.Permissions.Branch_Edit,
                CanDelete = ViewBag.Permissions.Branch_Delete,
                BranchGrid = _unitOfWork.Branch.GetAll().ToGrid()

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