using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Almotkaml.Reports;
using System.Linq;
namespace Almotkaml.ArtificialLimbs.Controllers
{
    public class StatuesReportController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public StatuesReportController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }
        // GET: ArtificialLimbsReport
        public ActionResult StatuesReport()
        {
            var model = Prepare();
            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult StatuesReport(StatuesReportModel model, FormCollection form)
        {
           
            var SearchValue = IntValue(form["Searchbtn"]);
          

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
               
                model.StatuesReportGrid = _unitOfWork.Reports.GetStatuesReport(model);

                return View(model);
            }

            //************************

         
            model.StatuesReportGrid = _unitOfWork.Reports.GetStatuesReport(model);
            return View(model);
        }

        // GET: NaturalTherapyReport
    
        //[AllowAnonymous]
      
        public ActionResult ViewReport(StatuesReportModel model, string id)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "StatuesReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(StatuesReport));

            }
            //model = Prepare();
            //model.DateFrom = "2019-07-01";
            //model.DateTo = "2019-07-30";
            //model.MedicineTypes = MedicineType.Electro;
            if (model.StatuesReportGrid.Count() == 0)
            {
                model.StatuesReportGrid = _unitOfWork.Reports.GetStatuesReport(model);

            }

            var datasources = new HashSet<StatuesReport>();

            foreach (var row in model.StatuesReportGrid)
            {
                datasources.Add(new StatuesReport()
                {
                    PatientNameCheck = model.PatientNameCheck,
                    PatientName=row.PatientName,
                    GenderCheck=model.GenderCheck,
                    Gender=row.Gender,
                    NationalityNumberCheck=model.NationalityNumberCheck,
                    NationalityNumber=row.NationalityNumber,
                    CityCheck=model.CityCheck,
                    City=row.City,
                    BirthDateCheck=model.BirthDateCheck,
                    BirthDate=row.BirthDate,
                    PhoneNumberCheck=model.PhoneNumberCheck,
                    PhoneNumber=row.PhoneNumber,
                    RegistrationDateCheck=model.RegistrationDateCheck,
                    RegistrationDate=row.RegistrationDate,
                    AmputationReasonCheck=model.AmputationReasonCheck,
                    AmputationReason=row.AmputationReason,
                    AmputationPartCheck=model.AmputationPartCheck,
                    AmputationPart=row.AmputationPart,
                    AmputationTypeCheck=model.AmputationTypeCheck,
                    AmputationType=row.AmputationType,
                    AmputationDateCheck=model.AmputationDateCheck,
                    AmputationDate=row.AmputationDate,
                    ShoeNOCheck=model.ShoeNOCheck,
                    ShoeNO=row.ShoeNO,
                    NoteCheck=model.NoteCheck,
                    Note=row.Note,
                    TechnicalNameCheck=model.TechnicalNameCheck,
                    TechnicalName=row.TechnicalName,
                    AssistantNameCheck=model.AssistantNameCheck,
                    AssistantName=row.AssistantName,
                    MeasurementDateCheck=model.MeasurementDateCheck,
                    MeasurementDate=row.MeasurementDate,
                    DeviceNameCheck=model.DeviceNameCheck,
                    DeviceName=row.DeviceName,
                    ReceiptDateCheck=model.ReceiptDateCheck,
                    ReceiptDate=row.ReceiptDate,

                   
                });
            }

            ReportDataSource rdc = new ReportDataSource("ReportDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", (DateTime.Parse(model.DateFrom).ToShortDateString())));
            reportParameters.Add(new ReportParameter("DateTo", (DateTime.Parse(model.DateTo).ToShortDateString())));
            reportParameters.Add(new ReportParameter("UserName", Session["UserName"].ToString()));
            //reportParameters.Add(new ReportParameter("Year", (DateTime.Parse(model.DateFrom).Year).ToString()));
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
         => ViewBag.Permissions.StatuesReport && permission;

        public StatuesReportModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<StatuesReportModel>(RequestState.NoPermission);

            //var DoctorShiftGrid = _unitOfWork.DoctorShift.GetAll().ToGrid();

            return new StatuesReportModel()
            {
                DateFrom = DateTime.Now.Date.ToString(),
                DateTo = DateTime.Now.Date.ToString(),
               

            };
        }

        public StatuesReportModel Refresh(StatuesReportModel model)
        {
            return model;
        }

    }
}
