using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class PatientExtensions
    {
        public static IEnumerable<PatientGridRow> ToGrid(this IEnumerable<Patient> patient)
            => patient.Select(d => new PatientGridRow()
            {
               PatientID = d.PatientID,
               PatientName =d.PatientName ,
               BirthDate =d.BirthDate.ToShortDateString () ,
               Gender =d.Gender,
               PhoneNumber = d.PhoneNumber ,
               NationalityID =d.NationalityID ,
               Nationality =d.Nationality?.Name ,
               NationalityNumber =d.NationalityNumber ,
               CityID = d.CityID,
               City=d.City?.Name ,
               PlaceId = d.PlaceId ,
               Place=d.Place?.Name ,
               RegistrationDate =d.RegistrationDate.ToShortDateString (),
               Note =d.Note ,
            });
    }
}