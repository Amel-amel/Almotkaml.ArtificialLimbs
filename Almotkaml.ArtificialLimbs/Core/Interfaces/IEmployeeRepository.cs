using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        bool NameExist(string FullName);
    }
}
