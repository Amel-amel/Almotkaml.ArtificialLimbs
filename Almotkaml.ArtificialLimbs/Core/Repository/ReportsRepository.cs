using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class ReportsRepository : Repository<Patient>, IReportsRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;


        public ReportsRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }

        public IEnumerable<StatuesReportGridRow> GetStatuesReport(StatuesReportModel model)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var ReportList = from PatientTbl in _Context.Patients
                             join DailyStatuesTbl in _Context.DailyStatues
                             on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                             select new StatuesReportGridRow
                                {
                                    PatientName = PatientTbl.PatientName,
                                    DeviceName = DailyStatuesTbl.device.ArabicName,
                                    MeasurementDate = DailyStatuesTbl.MeasurementDate,
                                    TechnicalName = DailyStatuesTbl.Technical.EmployeeName,
                                    ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                };

            return ReportList.ToList();

        }

        public IEnumerable<ReportDailyGridRow> GetDailyStatuesReport(ReportsModel model,string state)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);
  
            var ReportList = (from PatientTbl in _Context.Patients
                             join DailyStatuesTbl in _Context.DailyStatues
                             on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                              join DetyailsTbl in _Context.DailyDetails
                             on DailyStatuesTbl.DailyStatuesID equals DetyailsTbl.DailyStatuesID
                              into tbl  from endtbl in tbl.DefaultIfEmpty()
                              select new ReportDailyGridRow
                             {
                                 PatientName = PatientTbl.PatientName,
                                 ReferenceNo = DailyStatuesTbl.ReferenceNo.ToString(),
                                 DeviceName = DailyStatuesTbl.device.ArabicName,
                                 MeasurementDate = DailyStatuesTbl.MeasurementDate,
                                 TechnicalName = DailyStatuesTbl.Technical.EmployeeName ,
                                 AssistantName=  DailyStatuesTbl.TechnicalAssistant.EmployeeName,
                                 ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                 RegistrationDate = PatientTbl.RegistrationDate.ToString(),
                                 ShoeNO = endtbl.shoesNo,
                                 MeasurementModel = endtbl.MeasurementModel.ToString(),
                                 Part = endtbl.Part.ToString(),
                                 PartMeasure = endtbl.PartMeasure,
                             }).OrderBy(d=>d.ReferenceNo).ToList();

            if (state == "Recipt")
                ReportList = ReportList.Where(d => d.ReceiptDate != null).ToList();
            if (state == "Waiting")
                ReportList = ReportList.Where(d => d.ReceiptDate == null).ToList();

            if (_dateFrom != null)
            {
                ReportList = ReportList.Where(d => (Convert.ToDateTime(d.RegistrationDate)) >= _dateFrom).ToList();
            }

            if (_dateTo != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.RegistrationDate) <= _dateTo).ToList();
            }
            if (model.ShoeNO >0 )
            {
                ReportList = ReportList.Where(d => d.ShoeNO == model.ShoeNO).ToList();
            }

            if (model.MeasurementModel > 0)
            {
                ReportList = ReportList.Where(d => d.MeasurementModel == model.MeasurementModel.ToString()).ToList();
            }

            if (model.Part > 0)
            {
                ReportList = ReportList.Where(d => d.Part == model.Part.ToString()).ToList();
            }

            if (model.PartMeasure != null)
            {
                ReportList = ReportList.Where(d => d.PartMeasure == model.PartMeasure).ToList();
            }

            return ReportFormat(ReportList);

        }

        public IEnumerable<ReportDailyGridRow> ReportFormat(IEnumerable<ReportDailyGridRow> ReportList)
        {
            foreach (var row in ReportList)
            {
                if (row.ReceiptDate == null)
                    row.ReceiptDate = "عند الاتصال";
                if (row.AssistantName != null)
                    row.AssistantName = " + " + row.AssistantName;
            }
            return ReportList;
        }

        public IEnumerable<ReportAmputationGridRow> GetAmputationStatuesReport(ReportsModel model,string state)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var ReportList = (from PatientTbl in _Context.Patients
                             join AmputationStatuesTbl in _Context.AmputationStatues
                             on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                              join AmputationReasonsTbl in _Context.AmputationReasons
                              on AmputationStatuesTbl.AmputationReasonID equals AmputationReasonsTbl.AmputationReasonID
                              join tblDetails in _Context.AmputationDetails.DefaultIfEmpty()
                              on AmputationStatuesTbl.AmputationStatuesID equals tblDetails.AmputationStatuesID
                              into tbl from endtbl in tbl.DefaultIfEmpty()
                              select new ReportAmputationGridRow
                             {
                                 PatientName = PatientTbl.PatientName,
                                 ReferenceNo = AmputationStatuesTbl.ReferenceNo.ToString(),
                                 Gender  =PatientTbl.Gender.ToString(),
                                 NationalityNumber = PatientTbl.NationalityNumber,
                                 City = PatientTbl.City.Name,
                                 Place = PatientTbl.Place.Name,
                                 BirthDate = PatientTbl.BirthDate.Year.ToString(),
                                 PhoneNumber  = PatientTbl.PhoneNumber,
                                 RegistrationDate  = PatientTbl.RegistrationDate.Year +"-" + PatientTbl.RegistrationDate.Month + "-" + PatientTbl.RegistrationDate.Day,
                                AmputationReason = AmputationReasonsTbl.Reason ,
                                 AmputationPart  = AmputationStatuesTbl.AmputationPart.ToString(),
                                 AmputationType = AmputationStatuesTbl.amputationType.Name,
                                 AmputationDate = AmputationStatuesTbl.AmputationDate,
                                 ReceiptDate = AmputationStatuesTbl.ReceiptDate,
                                 Note = AmputationStatuesTbl.Note,
                                 ShoeNO = endtbl.shoesNo.ToString(),
                             }).OrderBy(d=>d.ReferenceNo).ToList();
            
            return ReportFilter(ReportList,model,state);

        }
        public string DateFormat(DateTime _date)
        {
            var Date=_date.Year +"-"+_date.Month + "-" +_date.Day;
            return Date;
        }
        public IEnumerable<ReportAmputationGridRow> ReportFilter(IEnumerable<ReportAmputationGridRow> ReportList, ReportsModel model, string state)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            if (state == "Recipt")
                ReportList = ReportList.Where(d => d.ReceiptDate != null).ToList();
            if (state == "Waiting")
                ReportList = ReportList.Where(d => d.ReceiptDate == null).ToList();

            if (_dateFrom != null)
            {
                ReportList = ReportList.Where(d => (Convert.ToDateTime(d.RegistrationDate)) >= _dateFrom).ToList();
            }

            if (_dateTo != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.RegistrationDate) <= _dateTo).ToList();
            }
            if (model.Gender > 0)
                ReportList = ReportList.Where(d => d.Gender == model.Gender.ToString()).ToList();


            if (model.AmputationTypeID > 0)
            {
                model.AmputationType = _context.AmputationTypes.Where(d => d.AmputationTypeID == model.AmputationTypeID).FirstOrDefault().Name;
                ReportList = ReportList.Where(d => d.AmputationType == model.AmputationType).ToList();
            }

            return ReportList;
        }

    }
}