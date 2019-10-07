using Almotkaml.ArtificialLimbs.Core;
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
    public class ActivityController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public ActivityController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        // GET: Activity
        [AllowAnonymous]
        public ActionResult Index()

        {
            var models = Prepare();
            return View(models);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ActivityModel model, FormCollection form)
        {
            GetPermission();
            var SearchValue = IntValue(form["Searchbtn"]);

            if (SearchValue > 0)
            {
                model.UserListItems = _unitOfWork.User.GetAll().ToList();
                model.UserActivityGridRows = _unitOfWork.Activities.Search(model).ToGrid();
                return View(model);
            }

            var models = Prepare();
            return View(models);
        }

         public ActivityModel Prepare()
        {
            GetPermission();
            return new ActivityModel()
            {
                UserListItems = _unitOfWork.User.GetAll().ToList(),
                UserActivityGridRows = _unitOfWork.Activities.GetAll().ToGrid(),

            };
        }


    }
}
