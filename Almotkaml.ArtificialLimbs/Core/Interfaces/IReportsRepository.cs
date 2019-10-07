using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almotkaml.ArtificialLimbs.Models;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IReportsRepository : IRepository<Patient>
    {
      IEnumerable<StatuesReportGridRow> GetStatuesReport(StatuesReportModel model);
      IEnumerable<ReportDailyGridRow> GetDailyStatuesReport(ReportsModel model,string state);
        IEnumerable<ReportAmputationGridRow> GetAmputationStatuesReport(ReportsModel model,string state);
    }
}
