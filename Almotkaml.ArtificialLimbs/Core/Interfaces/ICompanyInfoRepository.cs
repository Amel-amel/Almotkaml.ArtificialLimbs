using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface ICompanyInfoRepository : IRepository<Infoes>
    {
        CompanyInfoModel Get();
        void Save(CompanyInfos companyInfo);
    }
}
