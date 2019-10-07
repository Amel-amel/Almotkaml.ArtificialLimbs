using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(ArtificialLimbsDbContext context)
            : base(context)
        {

        }

        public bool NameExist(string FullName)
        {
            return _context.Employees.Where(e => e.EmployeeName == FullName).Any();
        }

        public override IEnumerable<Employee> GetAll()
        {
            return _context.Employees.Include(e => e.department).ToList();

        }
    }
}