using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Department department { get; set; }
        public int DepartmentID { get; set; }

        public virtual  ICollection<DailyStatues> DailyStatues { get; set; }
        public virtual ICollection<AmputationStatues> AmputationStatues { get; set; }
        public virtual ICollection<DailyStatues> DailyStatues1 { get; set; }
        public virtual ICollection<AmputationStatues> AmputationStatues1 { get; set; }

        public static Employee New(string _name,int _id)
        {
            Check.NotEmpty(_name, nameof(_name));

            var employee = new Employee()
            {
                EmployeeName = _name,
                DepartmentID= _id,
            };

            return employee;
        }

        public void Modify(int _id)
        {
            //Check.NotEmpty(_name, nameof(_name));

            //EmployeeName = _name;
            DepartmentID = _id;
        }
    }
}