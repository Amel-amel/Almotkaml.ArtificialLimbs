using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almotkaml.ArtificialLimbs.Models;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IStatisticsReportRepository : IRepository<Patient>
    {
     IEnumerable<DStatisticsReportGridRow> GetDStatisticsReport(StatisticsReportModel model);
     IEnumerable<AStatisticsReportGridRow> GetAStatisticsReport(StatisticsReportModel model);
    }
}
