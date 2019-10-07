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
using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public ReportsController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        // GET: ArtificialLimbsReport
        public ActionResult DailyReport()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult DailyReport(ReportsModel model, FormCollection form)
        {
            GetPermission();
            var SearchValue = IntValue(form["Searchbtn"]);
            var state = form["State"];

            if (ModelState.IsValid)
                if (form["PdfReportBtn"] == "PDF")
                {
                    return ViewDailyReport(state, model, "PDF");
                }
                else if (form["ExcelReportBtn"] == "EXCEL")
                {
                    return ViewDailyReport(state, model, "EXCEL");
                }
                else if (form["WordReportBtn"] == "WORD")
                {
                    return ViewDailyReport(state, model, "WORD");
                }


            if (SearchValue > 0)
            {
                if(ModelState.IsValid)
                    model.ReportDailyGrid = _unitOfWork.Reports.GetDailyStatuesReport(model,state);

                return View(Refresh(model));
            }

            //************************


            model.ReportDailyGrid = _unitOfWork.Reports.GetDailyStatuesReport(Prepare(), state);
            return View(Refresh(Refresh(model)));
        }

        // GET: ArtificialLimbsReport

        //[AllowAnonymous]

        public ActionResult ViewDailyReport(string state, ReportsModel model, string id)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "DailyReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(DailyReport));

            }
            //model = Prepare();
            //model.DateFrom = "2019-07-01";
            //model.DateTo = "2019-07-30";
            //model.MedicineTypes = MedicineType.Electro;
            if (model.ReportDailyGrid.Count() == 0)
            {
                    model.ReportDailyGrid = _unitOfWork.Reports.GetDailyStatuesReport(model,state);
            }

            var datasources = new HashSet<ReportDaily>();

            foreach (var row in model.ReportDailyGrid)
            {
                datasources.Add(new ReportDaily()
                {
                    PatientName = row.PatientName,
                    Gender = row.Gender,
                    NationalityNumber = row.NationalityNumber,
                    City = row.City,
                    BirthDate = row.BirthDate,
                    PhoneNumber = row.PhoneNumber,
                    RegistrationDate = row.RegistrationDate,
                    Note = row.Note,
                    TechnicalName = row.TechnicalName,
                    AssistantName = row.AssistantName,
                    MeasurementDate = row.MeasurementDate,
                    DeviceName = row.DeviceName,
                    ReceiptDate = row.ReceiptDate,
                    ReferenceNo = row.ReferenceNo ,
                    ShoeNO =row.ShoeNO,
                    MeasurementModel =row.MeasurementModel,
                    Part =row.Part,
                    PartMeasure=row.PartMeasure,
                });
            }

            ReportDataSource rdc = new ReportDataSource("DailyDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("ColSize",(5 - TotalCol(model)).ToString() + "in"));
            reportParameters.Add(new ReportParameter("ColumnFilter", BindColumnFilter(model)));
            reportParameters.Add(new ReportParameter("DateFrom", (DateTime.Parse(model.DateFrom).ToShortDateString())));
            reportParameters.Add(new ReportParameter("DateTo", (DateTime.Parse(model.DateTo).ToShortDateString())));
            reportParameters.Add(new ReportParameter("UserName", Session["UserName"].ToString()));
            reportParameters.Add(new ReportParameter("Title", ("حالات يومية لـسنة " + DateTime.Parse(model.DateFrom).Year).ToString()));
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

        public ActionResult AmputationReport()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult AmputationReport(ReportsModel model, FormCollection form)
        {
            GetPermission();
            var SearchValue = IntValue(form["Searchbtn"]);
            var state = form["State"];
                if (form["PdfReportBtn"] == "PDF")
                {
                    return ViewAmputationReport(state, model, "PDF");
                }
                else if (form["ExcelReportBtn"] == "EXCEL")
                {
                    return ViewAmputationReport(state, model, "EXCEL");
                }
                else if (form["WordReportBtn"] == "WORD")
                {
                    return ViewAmputationReport(state,model, "WORD");
                }


            if (SearchValue > 0)
            {
                model.ReportAmputationGrid = _unitOfWork.Reports.GetAmputationStatuesReport(model,state);
                return View(Refresh(model));
            }

            //************************


            model.ReportAmputationGrid = _unitOfWork.Reports.GetAmputationStatuesReport(Prepare(), state);
            return View(Refresh(model));
        }

        // GET: ArtificialLimbsReport

        //[AllowAnonymous]

        public ActionResult ViewAmputationReport(string state,ReportsModel model, string id)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "AmputationReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(AmputationReport));

            }
            //model = Prepare();
            //model.DateFrom = "2019-07-01";
            //model.DateTo = "2019-07-30";
            //model.MedicineTypes = MedicineType.Electro;
            if (model.ReportAmputationGrid.Count() == 0)
            {
                    model.ReportAmputationGrid = _unitOfWork.Reports.GetAmputationStatuesReport(model,state);

            }

            var datasources = new HashSet<ReportAmputation>();

            foreach (var row in model.ReportAmputationGrid)
            {
                datasources.Add(new ReportAmputation()
                {

                    PatientName = row.PatientName,
                    Gender = typeof(Gender).DisplayFieldName(row.Gender),
                    NationalityNumber = row.NationalityNumber,
                    City = row.City,
                    BirthDate = row.BirthDate,
                    PhoneNumber = row.PhoneNumber,
                    RegistrationDate = row.RegistrationDate,
                    AmputationReason = row.AmputationReason,
                    AmputationPart = typeof(AmputationPart).DisplayFieldName(row.AmputationPart),
                    AmputationType = row.AmputationType,
                    AmputationDate = row.AmputationDate,
                    ShoeNO= row.ShoeNO,
                    Note = row.Note,
                    ReferenceNo = row.ReferenceNo,
                });
            }

            ReportDataSource rdc = new ReportDataSource("AmputationDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", (DateTime.Parse(model.DateFrom).ToShortDateString())));
            reportParameters.Add(new ReportParameter("DateTo", (DateTime.Parse(model.DateTo).ToShortDateString())));
            reportParameters.Add(new ReportParameter("UserName", Session["UserName"].ToString()));
            reportParameters.Add(new ReportParameter("AmputationType", model.AmputationType));
            if(model.Gender == Gender.male)
                reportParameters.Add(new ReportParameter("Gender", "- ذكور"));
            else if (model.Gender == Gender.female )
                reportParameters.Add(new ReportParameter("Gender", "- إناث"));
            else
                reportParameters.Add(new ReportParameter("Gender", ""));
            reportParameters.Add(new ReportParameter("Title", (" لـسنة " + DateTime.Parse(model.DateFrom).Year).ToString()));
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
         => ViewBag.Permissions.Reports && permission;

        public ReportsModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<ReportsModel>(RequestState.NoPermission);

            //var DoctorShiftGrid = _unitOfWork.DoctorShift.GetAll().ToGrid();

            return new ReportsModel()
            {
                DateFrom = DateTime.Now.Date.ToString(),
                DateTo = DateTime.Now.Date.ToString(),
                AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList(),
            };
        }

        public ReportsModel Refresh(ReportsModel model)
        {
            model.AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList();
            return model;
        }

        public string BindColumnFilter(ReportsModel model)
        {
            string[] str = new string[4];
            if (model.CheckShoeNO)
                str[0] = "ShoeNO";
            if (model.CheckMeasurementModel)
                str[1] = "MeasurementModel";
            if (model.CheckPart)
                str[2] = "Part";
            if (model.CheckPartMeasure)
                str[3] = "PartMeasure";
            return new JavaScriptSerializer().Serialize(str);
        }
        public int TotalCol(ReportsModel model)
        {
            int Count = 0;
            if (model.CheckShoeNO)
                Count++;
            if (model.CheckMeasurementModel)
                Count++;
            if (model.CheckPart)
                Count++;
            if (model.CheckPartMeasure)
                Count++;
            return Count;
        }

    }
}
