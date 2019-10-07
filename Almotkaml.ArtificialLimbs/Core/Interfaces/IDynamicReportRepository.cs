using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IDynamicReportRepository : IRepository<DBNull>
    {
        IEnumerable<DynamicReportGridRow> GetDynamicReport(DynamicReportModel model, string state);
        string BindColumnFilter(DynamicReportModel model);
    }
}
