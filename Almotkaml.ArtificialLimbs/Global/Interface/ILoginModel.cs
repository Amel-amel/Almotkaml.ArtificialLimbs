using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Global.Interface
{
    public interface ILoginModel
    {
        string UserName { get; set; }

        string Password { get; set; }
    }
}
