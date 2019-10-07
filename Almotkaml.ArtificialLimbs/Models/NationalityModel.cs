using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class NationalityModel
    {
        public int NationalityId { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
            ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Nationality))]
        public string Name { get;  set; }

        public IEnumerable<NationalityGridRow> NationalityGrid { get; set; } = new HashSet<NationalityGridRow>();
        public IEnumerable<NationalityListItem> NationalityList { get; set; } = new HashSet<NationalityListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

    }

    public class NationalityGridRow
    {
        public int NationalityId { get; set; }
        public string Name { get; set; }
    }

    public class NationalityListItem
    {
        public int NationalityId { get; set; }
        public string Name { get; set; }
    }
}