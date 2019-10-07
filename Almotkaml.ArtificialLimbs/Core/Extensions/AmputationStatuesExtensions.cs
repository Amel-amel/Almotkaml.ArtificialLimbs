using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class AmputationStatuesExtensions
    {
        public static IEnumerable<AmputationStatuesGridRow> ToGrid(this IEnumerable<AmputationStatues> amputationStatues)
        => amputationStatues.Select(d => new AmputationStatuesGridRow()
        {
            AmputationStatuesID = d.AmputationStatuesID,
            PatientID = d.PatientID,
            PatientName = d.patient?.PatientName,
            TechnicalID = d.TechnicalID,
            TechnicalName = d.Technical?.EmployeeName,
            TechnicalAssistantID = d.TechnicalAssistantID,
            AssistantName = d.TechnicalAssistant?.EmployeeName,
            DeviceID = d.DeviceID,
            DeviceName = d.device?.ArabicName + " (" + d.device?.EnglishName + ")",
            MeasurementDate = d.MeasurementDate,
            FirstTestDate=d.FirstTestDate,
            SecondTestDate=d.SecondTestDate,
            ReceiptDate = d.ReceiptDate,
            ReferenceNo = d.ReferenceNo,
            AmputationTypeID=d.AmputationTypeID,
            AmputationPart=d.AmputationPart,
            AmputationReasonID = d.AmputationReasonID,
            AmputationReason =d.AmputationReason?.Reason,
            AmputationDate=d.AmputationDate,
            Note = d.Note,
        });
    }
}