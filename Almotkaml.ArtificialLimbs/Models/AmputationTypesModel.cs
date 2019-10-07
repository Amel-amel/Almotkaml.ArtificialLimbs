using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class AmputationTypesModel
    {
        public int AmputationTypesID { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public string AmputationName { get; set; }
        public IEnumerable<AmputationTypesGridRow> AmputationTypesGrid { get; set; } = new HashSet<AmputationTypesGridRow>();
        public IEnumerable<AmputationTypesListItem> AmputationTypesList { get; set; } = new HashSet<AmputationTypesListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class AmputationTypesGridRow
    {
        public int AmputationTypesID { get; set; }
        public string Name { get; set; }

    }

    public class AmputationTypesListItem
    {
        public int AmputationTypesID { get; set; }
        public string Name { get; set; }

    }
}
