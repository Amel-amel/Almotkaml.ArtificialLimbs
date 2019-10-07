using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Almotkaml.ArtificialLimbs.Core.Enum;
using System.Data.Entity.SqlServer;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class StatisticsReportRepository : Repository<Patient>, IStatisticsReportRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;

        public DateTime Age { get; set; }
        public StatisticsReportRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }


        public IEnumerable<DStatisticsReportGridRow> GetDStatisticsReport(StatisticsReportModel model)
        {
            var CUnderAgeM = MUnderAgeList(model);//----Male -- أقل من
            var CUnderAgeF = FUnderAgeList(model);//----FeMale -- أقل من
            var CAboveAgeM = MAboveAgeList(model);//----Male -- أكبر من
            var CAboveAgeF = FAboveAgeList(model);//----FeMale -- أكبر من

            foreach (DStatisticsReportGridRow MU in CUnderAgeM)
            {
                foreach (DStatisticsReportGridRow WU in CUnderAgeF)
                {
                    if (WU.DeviceID == MU.DeviceID)
                        MU.UnderAgeF = +WU.UnderAgeF;


                }
            }
            foreach (DStatisticsReportGridRow MA in CAboveAgeM)
            {
                foreach (DStatisticsReportGridRow WA in CAboveAgeF)
                {
                    if (WA.DeviceID == MA.DeviceID)
                        MA.AboveAgeF = +WA.AboveAgeF;

                }
            }

            foreach (DStatisticsReportGridRow MM in CUnderAgeM)
            {
                foreach (DStatisticsReportGridRow MM1 in CAboveAgeM)
                {
                    if (MM1.DeviceID == MM.DeviceID)
                    {

                        MM.AboveAgeF = +MM1.AboveAgeF;
                        MM.AboveAgeM = +MM1.AboveAgeM;

                    }
                }
            }


            var Result = CUnderAgeM.Concat(CAboveAgeM.Where(x => !CUnderAgeM.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result;


        }
        public IEnumerable<DStatisticsReportGridRow> MUnderAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year >= Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DevicesTbl.DeviceID } into P
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    //UnderAgeM = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                   // ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();

            


            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year >= Age.Year)
                               
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    UnderAgeM = 1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                }).ToList();


            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }





            foreach (DStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (DStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID))
                    {
                        RL.UnderAgeM = RL.UnderAgeM + RL2.UnderAgeM;
                       

                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();


        }
        public IEnumerable<DStatisticsReportGridRow> FUnderAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year >= Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName,  DevicesTbl.DeviceID } into P
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                   // UnderAgeF = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                    //ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();

            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year >= Age.Year)
                               
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    UnderAgeF = 1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }


            foreach (DStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (DStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID))
                    {
                        RL.UnderAgeF = RL.UnderAgeF + RL2.UnderAgeF;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();

           
        }
        public IEnumerable<DStatisticsReportGridRow> MAboveAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year < Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName,  DevicesTbl.DeviceID } into P
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                   // AboveAgeM = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                    //ReceiptDate = P.Key.ReceiptDate,

                                }).ToList();

            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year < Age.Year)
                                //group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DailyStatuesTbl.ReceiptDate, DevicesTbl.DeviceID } into P
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    AboveAgeM =1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReceiptDate = DailyStatuesTbl.ReceiptDate,

                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }


            foreach (DStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (DStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID))
                    {
                        RL.AboveAgeM = RL.AboveAgeM + RL2.AboveAgeM;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();

          
        }
        public IEnumerable<DStatisticsReportGridRow> FAboveAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year < Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DevicesTbl.DeviceID } into P
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                   // AboveAgeF = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                  //  ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();

            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join DailyStatuesTbl in _Context.DailyStatues
                                on PatientTbl.PatientID equals DailyStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year < Age.Year)
                                //group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DailyStatuesTbl.ReceiptDate, DevicesTbl.DeviceID } into P
                                select new DStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    AboveAgeF = 1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }


            foreach (DStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (DStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID))
                    {
                        RL.AboveAgeF = RL.AboveAgeF + RL2.AboveAgeF;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();
          

        }


   

        //****************** حالات البتر ************************
        public IEnumerable<AStatisticsReportGridRow> GetAStatisticsReport(StatisticsReportModel model)
        {
            var CUnderAgeM = AMUnderAgeList(model);//----Male -- أقل من
            var CUnderAgeF = AFUnderAgeList(model);//----FeMale -- أقل من
            var CAboveAgeM = AMAboveAgeList(model);//----Male -- أكبر من
            var CAboveAgeF = AFAboveAgeList(model);//----FeMale -- أكبر من

            foreach (AStatisticsReportGridRow MU in CUnderAgeM)
            {
                foreach (AStatisticsReportGridRow WU in CUnderAgeF)
                {
                    if ((WU.DeviceID == MU.DeviceID) && (WU.ReasonID == MU.ReasonID) )
                        MU.UnderAgeF = +WU.UnderAgeF;


                }
            }
            foreach (AStatisticsReportGridRow MA in CAboveAgeM)
            {
                foreach (AStatisticsReportGridRow WA in CAboveAgeF)
                {
                    if ((WA.ReasonID == MA.ReasonID) && (WA.DeviceID == MA.DeviceID))
                        MA.AboveAgeF = +WA.AboveAgeF;

                }
            }

            foreach (AStatisticsReportGridRow MM in CUnderAgeM)
            {
                foreach (AStatisticsReportGridRow MM1 in CAboveAgeM)
                {
                    if ((MM1.ReasonID == MM.ReasonID) && (MM1.DeviceID == MM.DeviceID))
                    {

                        MM.AboveAgeF = +MM1.AboveAgeF;
                        MM.AboveAgeM = +MM1.AboveAgeM;

                    }
                }
            }

            //var Result = CUnderAgeM.Concat(CUnderAgeF).Concat(CAboveAgeM).Concat(CAboveAgeF);
            var Result = CUnderAgeM.Concat(CAboveAgeM.Where(x => !CUnderAgeM.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result;
        }

        public IEnumerable<AStatisticsReportGridRow> AMUnderAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year >= Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason,  DevicesTbl.DeviceID, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    AmputationReason = P.Key.Reason,
                                    //UnderAgeM =1,
                                    DeviceID = P.Key.DeviceID,
                                    ReasonID = P.Key.AmputationReasonID,
                                  //  ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();

            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year >= Age.Year)
                                //group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, AmputationStatuesTbl.ReceiptDate, DevicesTbl.DeviceID, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    AmputationReason = AmputationStatuesTbl.AmputationReason.Reason,
                                    UnderAgeM = 1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReasonID = AmputationStatuesTbl.AmputationReason.AmputationReasonID,
                                    ReceiptDate = AmputationStatuesTbl.ReceiptDate,
                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }

            foreach (AStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (AStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID)&& (RL2.ReasonID == RL.ReasonID) )
                    {
                        RL.UnderAgeM = RL.UnderAgeM + RL2.UnderAgeM;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();
        


        }
        public IEnumerable<AStatisticsReportGridRow> AFUnderAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year >= Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, DevicesTbl.DeviceID, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    AmputationReason = P.Key.Reason,
                                   // UnderAgeF = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                    ReasonID = P.Key.AmputationReasonID,
                                   // ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();

            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year >= Age.Year)
                                //group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, DevicesTbl.DeviceID, AmputationStatuesTbl.ReceiptDate, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    AmputationReason = AmputationStatuesTbl.AmputationReason.Reason,
                                    UnderAgeF =1 ,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReasonID = AmputationStatuesTbl.AmputationReason.AmputationReasonID,
                                    ReceiptDate = AmputationStatuesTbl.ReceiptDate,
                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }

            foreach (AStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (AStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID) && (RL2.ReasonID == RL.ReasonID))
                    {
                        RL.UnderAgeF = RL.UnderAgeF + RL2.UnderAgeF;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();

        }
        public IEnumerable<AStatisticsReportGridRow> AMAboveAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year < Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, DevicesTbl.DeviceID, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    AmputationReason = P.Key.Reason,
                                   // AboveAgeM = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                    ReasonID = P.Key.AmputationReasonID,
                                  //  ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();


            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.male && PatientTbl.BirthDate.Year < Age.Year)
                                //group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, DevicesTbl.DeviceID, AmputationStatuesTbl.ReceiptDate, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    AmputationReason = AmputationStatuesTbl.AmputationReason.Reason,
                                    AboveAgeM = 1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReasonID = AmputationStatuesTbl.AmputationReason.AmputationReasonID,
                                    ReceiptDate = AmputationStatuesTbl.ReceiptDate,
                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }

            foreach (AStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (AStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID)&& (RL2.ReasonID == RL.ReasonID))
                    {
                        RL.AboveAgeM = RL.AboveAgeM + RL2.AboveAgeM;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();
        }
        public IEnumerable<AStatisticsReportGridRow> AFAboveAgeList(StatisticsReportModel model)
        {
            Age = DateTime.Now.AddYears(-(model.Age));
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);

            var UnderAgeList = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year < Age.Year)
                                group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, DevicesTbl.DeviceID , AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    AmputationReason = P.Key.Reason,
                                  //  AboveAgeF = P.Count(),
                                    DeviceID = P.Key.DeviceID,
                                    ReasonID = P.Key.AmputationReasonID,
                                    //ReceiptDate = P.Key.ReceiptDate,
                                }).ToList();

            var UnderAgeList2 = (from PatientTbl in _Context.Patients
                                join AmputationStatuesTbl in _Context.AmputationStatues
                                on PatientTbl.PatientID equals AmputationStatuesTbl.PatientID
                                join DevicesTbl in _Context.Devices
                                on AmputationStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                where (PatientTbl.Gender == Gender.female && PatientTbl.BirthDate.Year < Age.Year)
                                //group DevicesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.AmputationReason.Reason, DevicesTbl.DeviceID, AmputationStatuesTbl.ReceiptDate, AmputationStatuesTbl.AmputationReason.AmputationReasonID } into P
                                select new AStatisticsReportGridRow
                                {
                                    DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                    AmputationReason = AmputationStatuesTbl.AmputationReason.Reason,
                                    AboveAgeF = 1,
                                    DeviceID = DevicesTbl.DeviceID,
                                    ReasonID = AmputationStatuesTbl.AmputationReason.AmputationReasonID,
                                    ReceiptDate = AmputationStatuesTbl.ReceiptDate,

                                }).ToList();

            UnderAgeList2 = UnderAgeList2.Where(d => d.ReceiptDate != null).ToList();

            if (UnderAgeList2.Count() > 0)
            {
                if (_dateFrom != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                }

                if (_dateTo != null)
                {
                    UnderAgeList2 = UnderAgeList2.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                }
            }

            foreach (AStatisticsReportGridRow RL in UnderAgeList)
            {
                foreach (AStatisticsReportGridRow RL2 in UnderAgeList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID)&& (RL2.ReasonID == RL.ReasonID))
                    {
                        RL.AboveAgeF = RL.AboveAgeF + RL2.AboveAgeF;


                    }

                }
            }

            var Result = UnderAgeList.Concat(UnderAgeList2.Where(x => !UnderAgeList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            return Result.ToList();
        }

    }
}