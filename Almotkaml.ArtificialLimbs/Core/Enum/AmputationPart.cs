using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Enum
{
    public enum AmputationPart
    {
        [Display(ResourceType = typeof(titles), Name = nameof(titles.LT))]
        LT =1 ,
        [Display(ResourceType = typeof(titles), Name = nameof(titles.RT))]
        RT =2 ,
    }
}