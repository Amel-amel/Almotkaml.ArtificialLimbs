using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class AchievementsRepository : Repository<Patient>, IAchievementsRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;

        public DateTime Age { get; set; }
        public AchievementsRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }


        public IEnumerable<AchievementReportGridRow> GetAchievementReport(AchievementsModel model)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);
            int? DeviceId = model.DeviceID;

            var ReportList = AchievementReportList(model);
            var ReportList2 = AchievementReportList2(model);

            foreach (AchievementReportGridRow RL in ReportList)
            {
                foreach (AchievementReportGridRow RL2 in ReportList2)
                {
                    if ((RL2.DeviceID == RL.DeviceID))
                    {
                        RL.Total = RL.Total + RL2.Total;
                        //RL.ReceiptDate = RL2.ReceiptDate;

                    }

                }
            }


            var Result = ReportList.Concat(ReportList2.Where(x => !ReportList.Any(e => e.DeviceID.Equals(x.DeviceID))));

            //if (Result.Count() > 0)
            //{
            //    if (_dateFrom != null)
            //    {
            //        Result = Result.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
            //    }

            //    if (_dateTo != null)
            //    {
            //        Result = Result.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
            //    }
            //    if (DeviceId > 0)
            //    {
            //        Result = Result.Where(d => d.DeviceID == DeviceId).ToList();
            //    }
            //}


            return Result;


        }
        public IEnumerable<AchievementReportGridRow> AchievementReportList(AchievementsModel model)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);
            int? DeviceId = model.DeviceID;

            if (model.StatuesTypes == Enum.Types.Daily)
            {
                var ReportList = (from DailyStatuesTbl in _Context.DailyStatues
                                  join DevicesTbl in _Context.Devices
                                   on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                  where DailyStatuesTbl.ReceiptDate != null
                                  group DailyStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName,DailyStatuesTbl.DeviceID } into P
                                  select new AchievementReportGridRow
                                  {
                                      DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    
                                      DeviceID = P.Key.DeviceID,
                                      
                                  }).ToList();


                if (ReportList.Count() > 0)
                {
                   
                    if (DeviceId > 0)
                    {
                        ReportList = ReportList.Where(d => d.DeviceID == DeviceId).ToList();
                    }
                }


                return ReportList;
            }
            else if (model.StatuesTypes == Enum.Types.Amputation)
            {
                var ReportList = (from DevicesTbl in _Context.Devices
                                  join AmputationStatuesTbl in _Context.AmputationStatues
                                   on DevicesTbl.DeviceID equals AmputationStatuesTbl.DeviceID
                                  where AmputationStatuesTbl.ReceiptDate != null
                                  group AmputationStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.DeviceID } into P
                                  select new AchievementReportGridRow
                                  {
                                      DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                     
                                      DeviceID = P.Key.DeviceID,
                                      
                                  }).ToList();

                if (ReportList.Count() > 0)
                {

                    if (DeviceId > 0)
                    {
                        ReportList = ReportList.Where(d => d.DeviceID == DeviceId).ToList();
                    }
                }


                return ReportList;
            }
            else
            {
                var ListDaily = (from DevicesTbl in _Context.Devices
                                 join DailyStatuesTbl in _Context.DailyStatues
                                  on DevicesTbl.DeviceID equals DailyStatuesTbl.DeviceID
                                 where DailyStatuesTbl.ReceiptDate != null

                                 group DailyStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DailyStatuesTbl.DeviceID } into P
                                 select new AchievementReportGridRow
                                 {
                                     DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                    
                                     DeviceID = P.Key.DeviceID,
                                    
                                 }).ToList();

                var ListAmputation = (from DevicesTbl in _Context.Devices
                                      join AmputationStatuesTbl in _Context.AmputationStatues
                                       on DevicesTbl.DeviceID equals AmputationStatuesTbl.DeviceID
                                      where AmputationStatuesTbl.ReceiptDate != null
                                      group AmputationStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName,  AmputationStatuesTbl.DeviceID } into P
                                      select new AchievementReportGridRow
                                      {
                                          DeviceName = P.Key.ArabicName + " ( " + P.Key.EnglishName + " ) ",
                                          
                                          DeviceID = P.Key.DeviceID,
                                         
                                      }).ToList();



                var Result = ListDaily.Concat(ListAmputation.Where(x => !ListDaily.Any(e => e.DeviceID.Equals(x.DeviceID))));

                if (Result.Count() > 0)
                {

                    if (DeviceId > 0)
                    {
                        Result = Result.Where(d => d.DeviceID == DeviceId).ToList();
                    }
                }


                return Result;
            }





        }
        public IEnumerable<AchievementReportGridRow> AchievementReportList2(AchievementsModel model)
        {
            DateTime _dateFrom = DateTime.Parse(model.DateFrom);
            DateTime _dateTo = DateTime.Parse(model.DateTo);
            int? DeviceId = model.DeviceID;

            if (model.StatuesTypes == Enum.Types.Daily)
            {
                var ReportList = (from DailyStatuesTbl in _Context.DailyStatues
                                  join DevicesTbl in _Context.Devices
                                   on DailyStatuesTbl.DeviceID equals DevicesTbl.DeviceID
                                  where DailyStatuesTbl.ReceiptDate != null
                                  //group DailyStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DailyStatuesTbl.DeviceID } into P
                                  select new AchievementReportGridRow
                                  {
                                      DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                      Total = 1,
                                      DeviceID = DevicesTbl.DeviceID,
                                      ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                  }).ToList();


                if (ReportList.Count() > 0)
                {
                    if (_dateFrom != null)
                    {
                        ReportList = ReportList.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                    }

                    if (_dateTo != null)
                    {
                        ReportList = ReportList.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                    }
                    if (DeviceId > 0)
                    {
                        ReportList = ReportList.Where(d => d.DeviceID == DeviceId).ToList();
                    }
                }

                return ReportList;
            }
            else if (model.StatuesTypes == Enum.Types.Amputation)
            {
                var ReportList = (from DevicesTbl in _Context.Devices
                                  join AmputationStatuesTbl in _Context.AmputationStatues
                                   on DevicesTbl.DeviceID equals AmputationStatuesTbl.DeviceID
                                  where AmputationStatuesTbl.ReceiptDate != null
                                  //group AmputationStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.DeviceID } into P
                                  select new AchievementReportGridRow
                                  {
                                      DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                      Total = 1,
                                      DeviceID = DevicesTbl.DeviceID,
                                      ReceiptDate = AmputationStatuesTbl.ReceiptDate,
                                  }).ToList();

                if (ReportList.Count() > 0)
                {
                    if (_dateFrom != null)
                    {
                        ReportList = ReportList.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                    }

                    if (_dateTo != null)
                    {
                        ReportList = ReportList.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                    }
                    if (DeviceId > 0)
                    {
                        ReportList = ReportList.Where(d => d.DeviceID == DeviceId).ToList();
                    }
                }
                return ReportList;
            }
            else
            {
                var ListDaily = (from DevicesTbl in _Context.Devices
                                 join DailyStatuesTbl in _Context.DailyStatues
                                  on DevicesTbl.DeviceID equals DailyStatuesTbl.DeviceID
                                 where DailyStatuesTbl.ReceiptDate != null

                                 //group DailyStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, DailyStatuesTbl.DeviceID } into P
                                 select new AchievementReportGridRow
                                 {
                                     DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                     Total = 1,
                                     DeviceID = DevicesTbl.DeviceID,
                                     ReceiptDate = DailyStatuesTbl.ReceiptDate,
                                 }).ToList();

                var ListAmputation = (from DevicesTbl in _Context.Devices
                                      join AmputationStatuesTbl in _Context.AmputationStatues
                                       on DevicesTbl.DeviceID equals AmputationStatuesTbl.DeviceID
                                      where AmputationStatuesTbl.ReceiptDate != null
                                      //group AmputationStatuesTbl by new { DevicesTbl.EnglishName, DevicesTbl.ArabicName, AmputationStatuesTbl.DeviceID } into P
                                      select new AchievementReportGridRow
                                      {
                                          DeviceName = DevicesTbl.ArabicName + " ( " + DevicesTbl.EnglishName + " ) ",
                                          Total = 1,
                                          DeviceID = DevicesTbl.DeviceID,
                                          ReceiptDate = AmputationStatuesTbl.ReceiptDate,
                                      }).ToList();



                var Result = ListDaily.Concat(ListAmputation.Where(x => !ListDaily.Any(e => e.DeviceID.Equals(x.DeviceID))));


                if (Result.Count() > 0)
                {
                    if (_dateFrom != null)
                    {
                        Result = Result.Where(d => (Convert.ToDateTime(d.ReceiptDate)) >= _dateFrom).ToList();
                    }

                    if (_dateTo != null)
                    {
                        Result = Result.Where(d => Convert.ToDateTime(d.ReceiptDate) <= _dateTo).ToList();
                    }
                    if (DeviceId > 0)
                    {
                        Result = Result.Where(d => d.DeviceID == DeviceId).ToList();
                    }
                }

                return Result;
            }





        }


    }
}


