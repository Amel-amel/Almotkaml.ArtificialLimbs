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
    public class DynamicReportController : BaseController
    {
        private readonly ArtificialLimbsDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public DynamicReportController()
        {
            _context = new ArtificialLimbsDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: DynamicReport
        public ActionResult Index()
        {
            var model = Prepare();

            if (model == null)
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DynamicReportModel model,FormCollection form)
        {
            GetPermission();
            if (!HavePermission(ViewBag.Permissions.DynamicReport_Create))
                return RedirectToAction(nameof(ErrorController.PageNotFound), ControllerName(nameof(ErrorController)));

            var SearchValue = IntValue(form["Searchbtn"]);
            var state = form["State"];
            int totalSelected = 0;

            foreach (string key in form.AllKeys)
            {
                int subTotalSelected = form.GetValues(key).Where(x => x.Contains("true")).Count();
                totalSelected += subTotalSelected;
            }

           
            if (ModelState.IsValid)
                if (form["PdfReportBtn"] == "PDF")
                {
                    return ViewReport(state, model, "PDF", totalSelected);
                }
                else if (form["ExcelReportBtn"] == "EXCEL")
                {
                    return ViewReport(state, model, "EXCEL", totalSelected);
                }
                else if (form["WordReportBtn"] == "WORD")
                {
                    return ViewReport(state, model, "WORD", totalSelected);
                }


            if (SearchValue > 0)
            {
                if (ModelState.IsValid)
                    model.DynamicReportGrid = _unitOfWork.DynamicReport.GetDynamicReport(model, state);

                return View(Refresh(model));
            }

            model.DynamicReportGrid = _unitOfWork.DynamicReport.GetDynamicReport(Prepare(), state);
            return View(Refresh(model));
        }

        public ActionResult ViewReport(string state, DynamicReportModel model, string id,int TotalCol)
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "DynamicReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction(nameof(DynamicReportModel));

            }
            if (TotalCol > 7)
            {
                Note("لا يمكنك اختيار أكثر من 7 خيارات .");
                return View(Refresh(model));
            }
            TotalCol = 8 - TotalCol;

            if (model.DynamicReportGrid.Count() == 0)
            {
                model.DynamicReportGrid = _unitOfWork.DynamicReport.GetDynamicReport(model, state);
            }

            var datasources = new HashSet<DynamicReportGridRow>();

            foreach (var row in model.DynamicReportGrid)
            {
                datasources.Add(new DynamicReportGridRow()
                {
                    ReferenceNo = row.ReferenceNo,
                    patientName =row.patientName,
                    Gender = typeof(Gender).DisplayFieldName(row.Gender),
                    BirthDate =row.BirthDate ,
                    PhoneNumber =row.PhoneNumber,
                    NationalityNumber = row.NationalityNumber,
                    Nationality =row.Nationality,
                    City =row.City,
                    RegistrationDate =row.RegistrationDate,

                    Technical=row.Technical,
                    TechnicalAssistant =row.TechnicalAssistant,
                    device=row.device,

                    MeasurementDate=row.MeasurementDate,
                    FirstTestDate=row.FirstTestDate,
                    SecondTestDate=row.SecondTestDate,
                    ReceiptDate=row.ReceiptDate,

                    amputationType=row.amputationType,
                    AmputationPart = typeof(AmputationPart).DisplayFieldName(row.AmputationPart),
                    AmputationReason =row.AmputationReason,
                    AmputationDate=row.AmputationDate ,

                    ShoeNO = row.ShoeNO,
                    MeasurementModel = typeof(MeasurementModel).DisplayFieldName(row.MeasurementModel),
                    Part = row.Part,
                    PartMeasure = row.PartMeasure,

                    Note =row.Note,
                });
            }

           
            ReportDataSource rdc = new ReportDataSource("DynamicReportDataset", datasources);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            
            //var par = BindColumnFilter(model);
            string par =_unitOfWork.DynamicReport.BindColumnFilter(model);
            reportParameters.Add(new ReportParameter("ColumnFilter", par));
            reportParameters.Add(new ReportParameter("ColSize", TotalCol +"in"));
            reportParameters.Add(new ReportParameter("UserName", Session["UserName"].ToString()));
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

            _unitOfWork.Complete(p=>p.DynamicReport_Create);
            return File(renderedBytes, mimeType);
        }
      

        private bool HavePermission(bool permission = true)
        => ViewBag.Permissions.DynamicReport && permission;

        public DynamicReportModel Prepare()
        {
            GetPermission();
            if (!HavePermission())
                return Null<DynamicReportModel>(RequestState.NoPermission);

            return new DynamicReportModel()
            {
                AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList(),
                AmputationReasonList = _unitOfWork.AmputationReason.GetAll().ToList(),
                DeviceList = _unitOfWork.Device.GetAll().ToList(),
                NationalityList = _unitOfWork.Nationality.GetAll().ToList(),
                CityList = _unitOfWork.City.GetAll().ToList(),
                PatientGrid =_unitOfWork.Patient.GetAll().ToGrid(),
                EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid(),
            };
        }

        public DynamicReportModel Refresh(DynamicReportModel model)
        {
            model.AmputationTypesList = _unitOfWork.AmputationTypes.GetAll().ToList();
            model.AmputationReasonList = _unitOfWork.AmputationReason.GetAll().ToList();
            model.DeviceList = _unitOfWork.Device.GetAll().ToList();
            model.NationalityList = _unitOfWork.Nationality.GetAll().ToList();
            model.CityList = _unitOfWork.City.GetAll().ToList();
            model.PatientGrid = _unitOfWork.Patient.GetAll().ToGrid();
            model.EmployeeGrid = _unitOfWork.Employee.GetAll().ToGrid();

            if (model.PatientID > 0)
                model.PatientName = _unitOfWork.Patient.GetById(model.PatientID??0).PatientName;

            if (model.TechnicalID > 0)
                model.Technical = _unitOfWork.Employee.GetById(model.TechnicalID??0).EmployeeName;

            if (model.TechnicalAssistantID > 0)
                model.TechnicalAssistant = _unitOfWork.Employee.GetById(model.TechnicalAssistantID??0).EmployeeName;


            return model;
        }

    }
}
