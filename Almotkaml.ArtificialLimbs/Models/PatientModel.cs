using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class PatientModel
    {
        public int PatientID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public string PatientName { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Gender))]
        public Gender Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.BirthDate))]
        public string BirthDate { get; set; }

        [MaxLength(10, ErrorMessageResourceType = typeof(SharedMessages),
            ErrorMessageResourceName = nameof(SharedMessages.PhoneNumber))]
        [MinLength(10, ErrorMessageResourceType = typeof(SharedMessages),
            ErrorMessageResourceName = nameof(SharedMessages.PhoneNumber))]
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.PhoneNO))]
        public string PhoneNumber { get; set; }

        [MaxLength(12, ErrorMessageResourceType = typeof(SharedMessages),
            ErrorMessageResourceName = nameof(SharedMessages.NationalityNumber))]
        [MinLength(12, ErrorMessageResourceType = typeof(SharedMessages),
            ErrorMessageResourceName = nameof(SharedMessages.NationalityNumber))]
        //[Required(ErrorMessageResourceType = typeof(SharedMessages),
        //ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.NationalityNumber))]
        public string NationalityNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Nationality))]
        public int NationalityID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.CityName))]
        public int CityID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(SharedMessages),
        //ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Place))]
        public int? PlaceId { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public int DeviceId { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.RegistrationDate))]
        public string RegistrationDate { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Note))]
        public string Note { get; set; }

        public IEnumerable<PatientGridRow> PatientGrid = new HashSet<PatientGridRow>();
        public IEnumerable<CityListItem> CityList = new HashSet<CityListItem>();
        public IEnumerable<PlaceListItem> PlaceList = new HashSet<PlaceListItem>();
        public IEnumerable<NationalityListItem> NationalityList = new HashSet<NationalityListItem>();
        public IEnumerable<DeviceListItem> DeviceList = new HashSet<DeviceListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class PatientGridRow
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public Gender Gender { get; set; } 
        public string BirthDate { get; set; } 
        public string PhoneNumber { get; set; } 
        public string NationalityNumber { get; set; } 
        public int NationalityID { get; set; }
        public string Nationality { get; set; }
        public int CityID { get;  set; }
        public string City { get; set; }
        public int? PlaceId { get;  set; }
        public string Place { get; set; }
        public string RegistrationDate { get; set; } 
        public string Note { get; set; }
    }
    
    }