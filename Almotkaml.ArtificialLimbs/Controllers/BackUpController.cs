using Almotkaml.ArtificialLimbs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.IO;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class BackUpController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;
        ArtificialLimbsDbContext _context;
        public BackUpController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        // GET: BackUp
        public string Index()
        {
            var backUpFolder = ConfigurationManager.AppSettings["BackUpFolder"];
            Directory.CreateDirectory(backUpFolder);

            var path = backUpFolder + "B" + DateTime.Now.ToString("yyMMddHHmmss") + ".bak";
            _unitOfWork.Complete(p => p.BackUp_Create);
            return _unitOfWork.BackUp(path) ? path : "Failed";

            // return View();
        }
    }
}
