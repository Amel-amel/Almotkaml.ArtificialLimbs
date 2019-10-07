using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class CompanyInfoModel
    {
        public int CompanyInfosID { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.ShortName))]
        public string ShortName { get; set; }
        public string DivisonInGovernment { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.LongName))]
        public string LongName { get; set; }
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.EnglishName))]
        public string EnglishName { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Phone))]
        public string Phone { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Mobile))]
        public string Mobile { get; set; }
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Email))]
        public string Email { get; set; }
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Website))]
        public string Website { get; set; }
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Address))]
        public string Address { get; set; }

        public bool CanSubmit { get; set; }
        public IEnumerable<CompanyInfoGridRow> CompanyInfoGrid { get; set; } = new HashSet<CompanyInfoGridRow>();

        public bool CanEdit { get; set; }

    }

    public class CompanyInfoGridRow
    {
        public int CompanyInfosID { get; set; }
        public string ShortName { get; set; }
        public string DivisonInGovernment { get; set; }
        public string LongName { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public bool CanSubmit { get; set; }
    }
}