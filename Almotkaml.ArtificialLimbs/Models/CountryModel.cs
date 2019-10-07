using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Almotkaml.ArtificialLimbs.Models
{
    public class CountryModel
    {
        public int CountryId { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.CountryName))]
        public string Name { get; set; }
        public DateTime datetime { get; set; }
        public IEnumerable<CountryGridRow> CountryGrid { get; set; } = new HashSet<CountryGridRow>();
        public IEnumerable<CountryListItem> CountryList { get; set; } = new HashSet<CountryListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class CountryGridRow
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }

    public class CountryListItem
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}