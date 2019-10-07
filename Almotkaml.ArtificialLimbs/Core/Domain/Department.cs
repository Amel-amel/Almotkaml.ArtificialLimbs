using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Employee> Employees { get; } = new HashSet<Employee>();
        public ICollection<Device> Devices { get; } = new HashSet<Device>();

        public static Department New(string _name)
        {
            Check.NotEmpty(_name, nameof(_name));

            var department = new Department()
            {
                DepartmentName = _name,
            };
            
            return department;
        }

        public void Modify(string _name)
        {
            Check.NotEmpty(_name, nameof(_name));

            DepartmentName = _name;
        }
    }
}