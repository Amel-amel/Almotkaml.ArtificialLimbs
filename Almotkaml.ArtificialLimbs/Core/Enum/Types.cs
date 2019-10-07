using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Enum
{
    public enum Types
    {
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Daily))]
        Daily = 1, // يومية
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Amputation))]
        Amputation = 2,  // بتر
    }
}