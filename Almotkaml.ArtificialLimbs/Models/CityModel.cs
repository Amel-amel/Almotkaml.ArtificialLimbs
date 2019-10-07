using Almotkaml.ArtificialLimbs.Core.Resources;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class CityModel
    {
        public int CityId { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.CityName))]
        public string Name { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.ShouldSelected))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.CountryName))]
        public int CountryId { get; set; }

        public IEnumerable<CityGridRow> CityGrid { get; set; } = new HashSet<CityGridRow>();
        public IEnumerable<CityListItem> CityList { get; set; } = new HashSet<CityListItem>();

        public IEnumerable<CountryListItem> CountryList { get; set; } = new HashSet<CountryListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class CityGridRow
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
    }

    public class CityListItem
    {
        public int CityId { get; set; }
        public string Name { get; set; }
    }
}
