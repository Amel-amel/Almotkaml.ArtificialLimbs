using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class PlaceModel
    {
        public int PlaceId { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
         ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Place))]
        public string Name { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
         ErrorMessageResourceName = nameof(SharedMessages.ShouldSelected ))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.BranchName ))]
        public int BranchId { get;  set; }

        public IEnumerable<PlaceGridRow> PlaceGrid { get; set; } = new HashSet<PlaceGridRow>();
        public IEnumerable<PlaceListItem> PlaceList { get; set; } = new HashSet<PlaceListItem>();
        public IEnumerable<BranchListItem> BranchList { get; set; } = new HashSet<BranchListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

    }

    public class PlaceGridRow
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }

    public class PlaceListItem
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        
    }
}