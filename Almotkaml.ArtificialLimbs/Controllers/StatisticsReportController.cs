using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Almotkaml.Reports;
using System.Linq;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Global.Extensions;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class StatisticsReportController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public StatisticsReportController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        // GET: ArtificialLimbsStatisticsReport
        public ActionResult DStatisticsReport()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult DStatisticsReport(StatisticsReportModel model, FormCollection form)
        {
            GetPermission();
            var SearchValue = IntValue(form["Searchbtn"]);


            if (ModelState.IsValid)
                if (form["PdfReportBtn"] == "PDF")
                {
                    return ViewDStatisticsReport(model, "PDF");
                }
                else if (form["ExcelReportBtn"] == "EXCEL")
                {
                    return ViewDStatisticsReport(model, "EXCEL");
                }
                else if (form["WordReportBtn"] == "WORD")
                {
                    return ViewDStatisticsReport(model, "WORD");
                }


            if (SearchValue > 0)
            {
                if (ModelState.IsValid)
                    model.DStatisticsReportGrid = _unitOfWork.StatisticsReport.GetDStatisticsReport(model);
                if (model.Age > 10)
                {
                    model.Above = "  أكبر من " + model.Age + " سنة ";
                    model.Under = "  أقل من " + model.Age + " سنة ";
                }
                else
                {
                    model.Above = "  أكبر من " + model.Age + " سنوات ";
                    model.Under = "  أقل من " + model.Age + " سنوات ";
                }
                foreach (var row in model.DStatisticsReportGrid)
                {
                    row.Total = row.UnderAgeM + row.UnderAgeF + row.AboveAgeM + row.AboveAgeF;
                }

                return View(Refresh(model));
            }

            //************************


            model.DStatisticsReportGrid = _unitOfWork.StatisticsReport.GetDStatisticsReport(model);
            if (model.Age > 10)
            {
                model.Above = "  أكبر من " + model.Age + " سنة ";
                model.Under = "  أقل من " + model.Age + " سنة ";
            }
            else
            {
                model.Above = "  أكبر من " + model.Age + " سنوات ";
                model.Under = "  أقل من " + model.Age + " سنوات ";
            }
            foreach (var row in model.DStatisticsReportGrid)
            {
                row.Total = row.UnderAgeM + row.UnderAgeF + row.AboveAgeM + row.AboveAgeF;
            }

            return View(Refresh(model));
        }

        // GET: ArtificialLimbsReport

        //[AllowAnonymous]

        public ActionResult ViewDStatisticsReport(StatisticsReportModel model, string id)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "DStatisticsReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(DStatisticsReport));

            }

            if (model.DStatisticsReportGrid.Count() == 0)
            {
                model.DStatisticsReportGrid = _unitOfWork.StatisticsReport.GetDStatisticsReport(model);
            }

            var datasources = new HashSet<DStatisticsReport>();

            foreach (var row in model.DStatisticsReportGrid)
            {
                datasources.Add(new DStatisticsReport()
                {

                    DeviceName = row.DeviceName,
                    AboveAgeM = row.AboveAgeM,
                    AboveAgeF = row.AboveAgeF,
                    UnderAgeM = row.UnderAgeM,
                    UnderAgeF = row.UnderAgeF,
                });
            }

            if (model.Age > 10)
            {
                model.Above = "  أكبر من " + model.Age + " سنة ";
                model.Under = "  أقل من " + model.Age + " سنة ";
            }
            else
            {
                model.Above = "  أكبر من " + model.Age + " سنوات ";
                model.Under = "  أقل من " + model.Age + " سنوات ";
            }
            ReportDataSource rdc = new ReportDataSource("DSDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", (DateTime.Parse(model.DateFrom).ToShortDateString())));
            reportParameters.Add(new ReportParameter("DateTo", (DateTime.Parse(model.DateTo).ToShortDateString())));
            //reportParameters.Add(new ReportParameter("Age", model.Age.ToString()));
            reportParameters.Add(new ReportParameter("Above", model.Above.ToString()));
            reportParameters.Add(new ReportParameter("Under", model.Under.ToString()));
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

        // GET: ArtificialLimbsStatisticsReport
        public ActionResult AStatisticsReport()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult AStatisticsReport(StatisticsReportModel model, FormCollection form)
        {
            GetPermission();
            var SearchValue = IntValue(form["Searchbtn"]);


            if (ModelState.IsValid)
                if (form["PdfReportBtn"] == "PDF")
                {
                    return ViewAStatisticsReport(model, "PDF");
                }
                else if (form["ExcelReportBtn"] == "EXCEL")
                {
                    return ViewAStatisticsReport(model, "EXCEL");
                }
                else if (form["WordReportBtn"] == "WORD")
                {
                    return ViewAStatisticsReport(model, "WORD");
                }


            if (SearchValue > 0)
            {
                if (ModelState.IsValid)
                    model.AStatisticsReportGrid = _unitOfWork.StatisticsReport.GetAStatisticsReport(model);
                if (model.Age > 10)
                {
                    model.Above = "  أكبر من " + model.Age + " سنة ";
                    model.Under = "  أقل من " + model.Age + " سنة ";
                }
                else
                {
                    model.Above = "  أكبر من " + model.Age + " سنوات ";
                    model.Under = "  أقل من " + model.Age + " سنوات ";
                }
                foreach (var row in model.AStatisticsReportGrid)
                {
                    row.Total = row.UnderAgeM + row.UnderAgeF + row.AboveAgeM + row.AboveAgeF;
                }

                return View(Refresh(model));
            }

            //************************


            model.AStatisticsReportGrid = _unitOfWork.StatisticsReport.GetAStatisticsReport(model);
            if (model.Age > 10)
            {
                model.Above = "  أكبر من " + model.Age + " سنة ";
                model.Under = "  أقل من " + model.Age + " سنة ";
            }
            else
            {
                model.Above = "  أكبر من " + model.Age + " سنوات ";
                model.Under = "  أقل من " + model.Age + " سنوات ";
            }

            foreach (var row in model.AStatisticsReportGrid)
            {
                row.Total = row.UnderAgeM + row.UnderAgeF + row.AboveAgeM + row.AboveAgeF;
            }

            return View(Refresh(model));
        }

        // GET: ArtificialLimbsReport

        //[AllowAnonymous]

        public ActionResult ViewAStatisticsReport(StatisticsReportModel model, string id)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "AStatisticsReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(AStatisticsReport));

            }

            if (model.AStatisticsReportGrid.Count() == 0)
            {
                model.AStatisticsReportGrid = _unitOfWork.StatisticsReport.GetAStatisticsReport(model);
            }

            var datasources = new HashSet<AStatisticsReport>();

            foreach (var row in model.AStatisticsReportGrid)
            {
                datasources.Add(new AStatisticsReport()
                {

                    DeviceName = row.DeviceName,
                    AmputationReason = row.AmputationReason,
                    AboveAgeM = row.AboveAgeM,
                    AboveAgeF = row.AboveAgeF,
                    UnderAgeM = row.UnderAgeM,
                    UnderAgeF = row.UnderAgeF,
                });
            }

            if (model.Age > 10)
            {
                model.Above = " أكبر من " + model.Age + " سنة ";
                model.Under = " أقل من " + model.Age + " سنة ";
            }
            else
            {
                model.Above = "  أكبر من " + model.Age + " سنوات ";
                model.Under = "  أقل من " + model.Age + " سنوات ";
            }
            ReportDataSource rdc = new ReportDataSource("ASDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", (DateTime.Parse(model.DateFrom).ToShortDateString())));
            reportParameters.Add(new ReportParameter("DateTo", (DateTime.Parse(model.DateTo).ToShortDateString())));
            //reportParameters.Add(new ReportParameter("Age", model.Age.ToString()));
            reportParameters.Add(new ReportParameter("Above", model.Above.ToString()));
            reportParameters.Add(new ReportParameter("Under", model.Under.ToString()));
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
         => ViewBag.Permissions.StatisticsReport && permission;

        public StatisticsReportModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<StatisticsReportModel>(RequestState.NoPermission);

            return new StatisticsReportModel()
            {
                DateFrom = DateTime.Now.Date.ToString(),
                DateTo = DateTime.Now.Date.ToString(),
                Above = " أكبر من __ سنة ",
                Under = " أقل من __ سنة ",
                AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList(),
            };
        }

        public StatisticsReportModel Refresh(StatisticsReportModel model)
        {
            model.AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList();
            if (!model.AStatisticsReportGrid.Any())
            {

            }
            if (!model.DStatisticsReportGrid.Any())
            {

            }
            return model;
        }
    }
}
