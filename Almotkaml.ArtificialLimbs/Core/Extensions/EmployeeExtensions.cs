using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class EmployeeExtensions
    {
       
            public static IEnumerable<EmployeeListItem> ToList(this IEnumerable<Employee> employee)
               => employee.Select(d => new EmployeeListItem()
               {
                  EmployeeID = d.EmployeeID,
                  EmployeeName = d.EmployeeName ,
               });

            public static IEnumerable<EmployeeGridRow> ToGrid(this IEnumerable<Employee> employee)
               => employee.Select(d => new EmployeeGridRow()
               {
                   EmployeeID = d.EmployeeID,
                   EmployeeName = d.EmployeeName,
                   DepartmentID= d.DepartmentID,
                   DepartmentName = d.department?.DepartmentName,
               });
        
    }
}