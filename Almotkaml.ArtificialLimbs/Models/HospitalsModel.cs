using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class HospitalsModel
    {
        public int HospitalsId { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
       ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
       [Display(ResourceType = typeof(titles), Name = nameof(titles.HospitalName))]
        public string Name { get; set; }
        public IEnumerable<HospitalsGridRow> HospitalsGrid { get; set; } = new HashSet<HospitalsGridRow>();
        public IEnumerable<HospitalsListItem> HospitalsList { get; set; } = new HashSet<HospitalsListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class HospitalsGridRow
    {
        public int HospitalsId { get; set; }
        public string Name { get; set; }
     
    }

    public class HospitalsListItem
    {
        public int HospitalsId { get; set; }
        public string Name { get; set; }

    }
}