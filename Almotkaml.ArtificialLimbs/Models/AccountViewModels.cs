using Almotkaml.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class LoginLoginModel
    {
        [Required]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.UserName))]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Password))]
        public string Password { get; set; }
    }
}
