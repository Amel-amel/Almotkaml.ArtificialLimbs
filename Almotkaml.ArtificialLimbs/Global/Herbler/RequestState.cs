using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public enum RequestState
    {
        Valid = 0,
        NotLogged = 1,
        NotFound = 2,
        BadRequest = 3,
        NoPermission = 4,
        Invalid = 5
    }
}