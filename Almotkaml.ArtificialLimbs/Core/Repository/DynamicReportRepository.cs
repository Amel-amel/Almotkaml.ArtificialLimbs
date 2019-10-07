using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Core.Enum;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class DynamicReportRepository : Repository<DBNull>, IDynamicReportRepository
    {
        private ArtificialLimbsDbContext Context { get; }

        internal DynamicReportRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            Context = context;
        }

        public IEnumerable<DynamicReportGridRow> GetDynamicReport(DynamicReportModel model, string state)
        {
            var ReportList = _context.AmputationStatues
                                    .Include(s => s.patient)
                                    .Include(s => s.AmputationReason)
                                    .Include(s => s.amputationType)
                                    .Include(s => s.Technical)
                                    .Include(s => s.TechnicalAssistant)
                                    .Include(s => s.device)
                                    .Select(d => new DynamicReportGridRow
                                    {
                                        ReferenceNo = d.ReferenceNo,
                                        PatientID = d.PatientID,
                                        patientName = d.patient.PatientName,
                                        Gender = d.patient.Gender.ToString(),
                                        BirthDate = d.patient.BirthDate.ToString(),
                                        PhoneNumber = d.patient.PhoneNumber,
                                        Nationality = d.patient.Nationality.Name,
                                        NationalityNumber = d.patient.NationalityNumber,
                                        City = d.patient.City.Name,
                                        RegistrationDate = d.patient.RegistrationDate.ToString(),

                                        DeviceID = d.DeviceID,
                                        device = d.device.ArabicName,
                                        TechnicalID = d.TechnicalID,
                                        Technical = d.Technical.EmployeeName,
                                        TechnicalAssistantID = d.TechnicalAssistantID,
                                        TechnicalAssistant = d.TechnicalAssistant.EmployeeName,

                                        MeasurementDate = d.MeasurementDate,
                                        FirstTestDate = d.FirstTestDate,
                                        SecondTestDate = d.SecondTestDate,
                                        ReceiptDate = d.ReceiptDate,

                                        //Part =d.AmputationDetails.Select(b => b.Part).FirstOrDefault().ToString(),
                                        //PartMeasure = d.AmputationDetails.AsQueryable().Select(b => b.PartMeasure).FirstOrDefault(),

                                        amputationType = d.amputationType.Name,
                                        AmputationPart = d.AmputationPart.ToString(),
                                        AmputationReason = d.AmputationReason.Reason,
                                        AmputationDate = d.AmputationDate,

                                       // ShoeNO =d.shoesNo,
                                       // MeasurementModel = d.MeasurementModel.ToString(),
                                        Part = d.AmputationDetails .ToList().FirstOrDefault().Part.ToString(),
                                        PartMeasure = d.AmputationDetails .ToList().FirstOrDefault().PartMeasure,
                                        ShoeNO = d.AmputationDetails.AsQueryable().Select(b => b.shoesNo) .FirstOrDefault(),
                                        MeasurementModel = d.AmputationDetails.AsQueryable().Select(b => b.MeasurementModel).FirstOrDefault().ToString(),
                                        Note = d.Note + " " + d.Note+ " " + d.patient.Note,
                                    }).OrderBy(d=>d.ReferenceNo);
           
            return ReportFilter(ReportList,model,state);
        }

        public IEnumerable<DynamicReportGridRow> ReportFilter(IEnumerable<DynamicReportGridRow> ReportList, DynamicReportModel model, string state)
       {
            DateTime? _dateFrom=new DateTime();
            DateTime? _dateTo=new DateTime();
            var _MeasurementModel = model.MeasurementModel.ToString();
            var _AmputationPart = model.AmputationPart.ToString();
            var _Part = model.Part.ToString();
           

            if (model.RegistrationDateFrom != null)
            {
                _dateFrom = DateTime.Parse(model.RegistrationDateFrom);//ReportList = ReportList.Where(d => (Convert.ToDateTime(d.RegistrationDate)) >= _dateFrom).ToList();
            }
            if (model.RegistrationDateTo != null)
            {
                _dateTo = DateTime.Parse(model.RegistrationDateTo);//ReportList = ReportList.Where(d => (Convert.ToDateTime(d.RegistrationDate)) >= _dateFrom).ToList();
            }

            if (state == "Recipt")
                ReportList = ReportList.Where(d => d.ReceiptDate != null).ToList();
            if (state == "Waiting")
                ReportList = ReportList.Where(d => d.ReceiptDate == null).ToList();


            if (model.ReferenceNo != null)
            {
                ReportList = ReportList.Where(d => d.ReferenceNo == model.ReferenceNo).ToList();
            }
            if (model.PatientID > 0 && model.PatientID != null)
            {
                ReportList = ReportList.Where(d => d.PatientID == model.PatientID).ToList();
            }
            if (model.Gender > 0)
            {
                ReportList = ReportList.Where(d => d.Gender == model.Gender.ToString()).ToList();
            }
            if (model.BirthDate != null)
            {
                ReportList = ReportList.Where(d =>  d.BirthDate == model.BirthDate).ToList();
            }
            if (model.PhoneNumber != null)
            {
                ReportList = ReportList.Where(d => d.PhoneNumber == model.PhoneNumber).ToList();
            }
            if (model.NationalityID != null)
            {
                ReportList = ReportList.Where(d => d.Nationality == model.NationalityID).ToList();
            }
            if (model.NationalityNumber != null)
            {
                ReportList = ReportList.Where(d => d.NationalityNumber == model.NationalityNumber).ToList();
            }
            if (model.CityID != null)
            {
               ReportList = ReportList.Where(d => d.City == model.CityID).ToList();
            }

            if (model.RegistrationDate != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.RegistrationDate) == _dateTo).ToList();
            }
            if (_dateFrom > DateTime.MinValue)
            {
                ReportList = ReportList.Where(d => (Convert.ToDateTime(d.RegistrationDate)) >= _dateFrom).ToList();
            }

            if (_dateTo > DateTime.MinValue)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.RegistrationDate) <= _dateTo).ToList();
            }

            if (model.DeviceID != null)
            {
                ReportList = ReportList.Where(d => d.DeviceID == model.DeviceID).ToList();
            }
            if (model.TechnicalID > 0 && model.TechnicalID!=null)
            {
                ReportList = ReportList.Where(d => d.TechnicalID == model.TechnicalID).ToList();
            }
            if (model.TechnicalAssistantID != null)
            {
                ReportList = ReportList.Where(d => d.TechnicalAssistantID == model.TechnicalAssistantID).ToList();
            }

            if (model.MeasurementDate != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.MeasurementDate) == Convert.ToDateTime(model.MeasurementDate)).ToList();
            }
            if (model.FirstTestDate != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.FirstTestDate) == Convert.ToDateTime(model.FirstTestDate)).ToList();
            }
            if (model.SecondTestDate != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.SecondTestDate) == Convert.ToDateTime(model.SecondTestDate)).ToList();
            }
            if (model.ReceiptDate != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.ReceiptDate) == Convert.ToDateTime(model.ReceiptDate)).ToList();
            }

            if (model.amputationType != null)
            {
                var _Type = _context.AmputationTypes.Find(Convert.ToInt32((model.amputationType))).Name;
                ReportList = ReportList.Where(d => d.amputationType == _Type).ToList();
            }
            if (_AmputationPart != null &&  _AmputationPart != "0")
            {
                ReportList = ReportList.Where(d => d.AmputationPart == _AmputationPart).ToList();
            }
            if (model.AmputationReason != null)
            {
                var _Reason = _context.AmputationReasons.Find(Convert.ToInt32(model.AmputationReason)).Reason;
                ReportList = ReportList.Where(d => d.AmputationReason == _Reason).ToList();
            }
            if (model.AmputationDate != null)
            {
                ReportList = ReportList.Where(d => Convert.ToDateTime(d.AmputationDate) == Convert.ToDateTime(model.AmputationDate)).ToList();
            }

            if (model.ShoeNO != null)
            {
                ReportList = ReportList.Where(d => d.ShoeNO == model.ShoeNO).ToList();
            }

            if (_MeasurementModel != null && _MeasurementModel != "0")
            {
                ReportList = ReportList.Where(d => d.MeasurementModel == _MeasurementModel).ToList();
            }

            if (_Part !=null && _Part != "0")
            {
                ReportList = ReportList.Where(d => d.Part == _Part).ToList();
            }

            if (model.PartMeasure != null)
            {
                ReportList = ReportList.Where(d => d.PartMeasure == model.PartMeasure).ToList();
            }

            if (model.Note != null)
            {
                ReportList = ReportList.Where(d => d.Note == model.Note).ToList();
            }
            return ReportList;
        }

        public string BindColumnFilter(DynamicReportModel model)
        {
            string[] str = new string[25];
            if (model.CheckMeasurementDate)
                str[0] = "MDate";
            if (model.CheckReceiptDate)
                str[1] = "RDate";
            if (model.CheckFirstTestDate)
                str[2] = "FTDate";
            if (model.CheckSecondTestDate)
                str[3] = "STDate";
            if (model.CheckNote)
                str[4] = "Note";
            if (model.CheckGender)
                str[5] = "Gender";
            if (model.CheckBirthDate)
                str[6] = "YearOfBirth";
            if (model.CheckPhoneNumber)
                str[7] = "PhoneNO";
            if (model.CheckNationality)
                str[8] = "Nationality";
            if (model.CheckNationalityNumber)
                str[9] = "NationNumber";
            if (model.CheckCity)
                str[10] = "Address";
            if (model.CheckRegistrationDate)
                str[11] = "RegistDate";
            if (model.CheckTechnical)
                str[12] = "Technical";
            if (model.CheckDeviceID)
                str[13] = "Device";
            if (model.CheckamputationType)
                str[14] = "Type";
            if (model.CheckAmputationPart)
                str[15] = "AmputationPart";
            if (model.CheckAmputationReason)
                str[16] = "Reason";
            if (model.CheckAmputationDate)
                str[17] = "AmpDate";
            if (model.CheckShoeNO)
                str[18] = "ShoeNO";
            if (model.CheckMeasurementModel)
                str[19] = "MeasurementModel";
            if (model.CheckPart)
                str[20] = "Part";
            if (model.CheckPartMeasure)
                str[21] = "PartMeasure";
            return new JavaScriptSerializer().Serialize(str);
        }

    }

}