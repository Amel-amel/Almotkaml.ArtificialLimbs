using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public HomeController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Index()

        {
            var model = Prepare();
            return View(model);
        }

      
        public HomeModel Prepare()
        {
            GetPermission();
            int year = DateTime.Now.Year;
            return new HomeModel()
            {
                PatientCount = _unitOfWork.Patient.GetPatientCount(),
                NewPatientCount = _unitOfWork.Patient.GetNewPatientCount(),
                MenCount = _unitOfWork.Patient.GetMenCount(),
                WomenCount = _unitOfWork.Patient.GetWomenCount(),
                ChildrenCount = _unitOfWork.Patient.GetChildrenCount(),
                AmpPatientCount = _unitOfWork.AmputationStatues.GetAll().Where(a=>a.patient.RegistrationDate.Year == year ).Count(),
            };
        }
    }
}