using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Almotkaml.Reports.DataSet;
using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class AchievementsController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public AchievementsController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: AchievementsReport
        public ActionResult Index()
        {
            var model = Prepare();

            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AchievementsModel model, FormCollection form)
        {
            GetPermission();
            var SearchValue = IntValue(form["Searchbtn"]);


            if (ModelState.IsValid)
                if (form["PdfReportBtn"] == "PDF")
                {
                    return ViewReport(model, "PDF");
                }
                else if (form["ExcelReportBtn"] == "EXCEL")
                {
                    return ViewReport(model, "EXCEL");
                }
                else if (form["WordReportBtn"] == "WORD")
                {
                    return ViewReport(model, "WORD");
                }


            if (SearchValue > 0)
            {
                if (ModelState.IsValid)
                    model.AchievementReportGrid = _unitOfWork.AchievementsReport.GetAchievementReport(model);
              
              
               

                return View(Refresh(model));
            }

            //************************


            model.AchievementReportGrid = _unitOfWork.AchievementsReport.GetAchievementReport(model);
          
            return View(Refresh(model));
        }

        public ActionResult ViewReport(AchievementsModel model, string id)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "AchievementReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(Index));

            }

            if (model.AchievementReportGrid.Count() == 0)
            {
                model.AchievementReportGrid = _unitOfWork.AchievementsReport.GetAchievementReport(model);
            }

            var datasources = new HashSet<AchievementReport>();

            foreach (var row in model.AchievementReportGrid)
            {
                datasources.Add(new AchievementReport()
                {

                    DeviceID  = row.DeviceID ,
                    DeviceName = row.DeviceName,
                    ReceiptDate = row.ReceiptDate,
                    Total = row.Total,
                 
                });
            }

         
            ReportDataSource rdc = new ReportDataSource("AchievementDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", (DateTime.Parse(model.DateFrom).ToShortDateString())));
            reportParameters.Add(new ReportParameter("DateTo", (DateTime.Parse(model.DateTo).ToShortDateString())));

            reportParameters.Add(new ReportParameter("UserName", Session["UserName"].ToString()));
            reportParameters.Add(new ReportParameter("Title", (" شهر  " + DateTime.Parse(model.DateFrom).Month).ToString() + (" لـسنة  " + DateTime.Parse(model.DateFrom).Year).ToString()));
            lr.SetParameters(reportParameters);
            lr.DataSources.Add(rdc);
            string reportType = id;
            string mimeType;
            string encoding;
            string filenameextention;
            string deviceinfo =
                "<DeviceInfo>" +
                "<OutPutFormat>" + "PDF" + "</OutPutFormat>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] stream;
            byte[] renderedBytes;
            renderedBytes = lr.Render(
                reportType,
                deviceinfo,
                out mimeType,
                out encoding,
                out filenameextention,
                out stream,
                out warnings)
               ;

            return File(renderedBytes, mimeType);
        }

        private bool HavePermission(bool permission = true)
=> ViewBag.Permissions.Achievements && permission;

        public AchievementsModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<AchievementsModel>(RequestState.NoPermission);

            return new AchievementsModel()
            {
                DeviceList = _unitOfWork.Device.GetAll().ToList(),

            };
        }

        public AchievementsModel Refresh(AchievementsModel model)
        {
            model.DeviceList = _unitOfWork.Device.GetAll().ToList();
                      
            
            return model;
        }
    }
}
