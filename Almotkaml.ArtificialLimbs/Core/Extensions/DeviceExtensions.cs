using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class DeviceExtensions
    {
       
            public static IEnumerable<DeviceListItem> ToList(this IEnumerable<Device> device)
               => device.Select(d => new DeviceListItem()
               {
                  DeviceID = d.DeviceID,
                  Name = d.ArabicName + " (" +d.EnglishName+")" ,
               });

            public static IEnumerable<DeviceGridRow> ToGrid(this IEnumerable<Device> device)
               => device.Select(d => new DeviceGridRow()
               {
                   DeviceID = d.DeviceID,
                   ArabicName = d.ArabicName,
                   EnglishName = d.EnglishName ,
                   DepartmentID = d.DepartmentID,
                   Type = d.Type ,
                   DepartmentName = d.department?.DepartmentName ,
               });
        
    }
}