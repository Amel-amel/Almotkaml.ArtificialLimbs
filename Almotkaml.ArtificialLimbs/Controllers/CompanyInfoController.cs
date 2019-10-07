using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class CompanyInfoController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public CompanyInfoController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: CompanyInfo
        public ActionResult Index()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            model.CanEdit = ViewBag.Permissions.CompanyInfo_Edit;
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CompanyInfoModel model, HttpPostedFileBase file, string save, string savedModel)
        {
            if (save == null)
            {
                ModelState.Clear();
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);

            if (!Save(model))
                return View(model);

            SuccessEditNote();
            return RedirectToAction("Index");
        }

        private bool HavePermission(bool permission = true)
           => ViewBag.Permissions.CompanyInfo && permission;

        
        public bool Save(CompanyInfoModel model)
        {
            var companyInfo = new CompanyInfos()
            {
                Email = model.Email,
                EnglishName = model.EnglishName,
                LongName = model.LongName,
                Mobile = model.Mobile,
                Address = model.Address,
                ShortName = model.ShortName,
                Website = model.Website,
                Phone = model.Phone,

            };

            _unitOfWork.CompanyInfos.Save(companyInfo);

            _unitOfWork.Complete(p => p.CompanyInfo_Edit);


            return true /*SuccessEdit()*/;
        }

        private CompanyInfoModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<CompanyInfoModel>(RequestState.NoPermission);

            return _unitOfWork.CompanyInfos.Get();

        }


      
    }
}
