using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Enum
{
//نموذج القياس
//add by ali alherbade 30-07-2019
    public enum MeasurementModel
    {
        [Display(ResourceType = typeof(titles), Name = nameof(titles.leg))]
        leg = 1, // رجل 
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Foot))]
        Foot = 2,// قدم
        [Display(ResourceType = typeof(titles), Name = nameof(titles.sole))]
        sole = 3,// باطن القدم
    }

    public enum MeasurementNumberModel
    {
        L1 = 1,
        L2 = 2,
        L3= 3,
        L4 = 4,
        D1= 5,
        D2 = 6,
        D3 = 7,
        D4 = 8,
        D5 = 9,
        D6 = 10,
        D7 = 11,
        D8 = 12,
        D9 = 13,
        D10 = 14,
        D11 = 15,
        D12 = 16,
        D13 = 17,
    }
}