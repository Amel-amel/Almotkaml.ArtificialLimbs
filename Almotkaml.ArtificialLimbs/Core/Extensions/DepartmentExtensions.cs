using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class DepartmentExtensions
    {
       
            public static IEnumerable<DepartmentListItem> ToList(this IEnumerable<Department> department)
               => department.Select(d => new DepartmentListItem()
               {
                  DepartmentID=d.DepartmentID,
                  DepartmentName =d.DepartmentName,
               });

            public static IEnumerable<DepartmentGridRow> ToGrid(this IEnumerable<Department> department)
               => department.Select(d => new DepartmentGridRow()
               {
                   DepartmentID = d.DepartmentID,
                   DepartmentName = d.DepartmentName,
               });
        
    }
}