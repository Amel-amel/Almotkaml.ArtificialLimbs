using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class DailyStatuesExtensions
    {

        public static IEnumerable<DailyStatuesGridRow> ToGrid(this IEnumerable<DailyStatues> dailyStatues)
          => dailyStatues.Select(d => new DailyStatuesGridRow()
          {
             DailyStatuesID = d.DailyStatuesID,
             PatientIID = d.PatientID ,
             PatientName =d.patient?.PatientName ,
             TechnicalID =d.TechnicalID ,
             TechnicalName =d.Technical?.EmployeeName ,
             TechnicalAssistantID= d.TechnicalAssistantID ,
             AssistantName = d.TechnicalAssistant?.EmployeeName,
             DeviceID=d.device.DeviceID,
             DeviceName =d.device?.ArabicName +" ("+ d.device?.EnglishName + ")" ,
             MeasurementDate= d.MeasurementDate,
             ReceiptDate =d.ReceiptDate ,
             ReferenceNo =d.ReferenceNo,
             Note=d.Note ,
          });

    }
}