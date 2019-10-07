using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Enum
{
    public enum Gender
    {
        [Display(ResourceType = typeof(titles), Name = nameof(titles.male))]
        male = 1,
        [Display(ResourceType = typeof(titles), Name = nameof(titles.female))]
        female = 2,
    }
}