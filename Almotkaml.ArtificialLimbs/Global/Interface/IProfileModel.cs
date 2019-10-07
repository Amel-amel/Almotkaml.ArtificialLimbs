using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Global.Interface
{
    public interface IProfileModel
    {
        string UserName { get; set; }
        string OldPassword { get; set; }
        string NewPassword { get; set; }
        string ConfirmPassword { get; set; }
        bool ChangePassword { get; set; }
    }
}
