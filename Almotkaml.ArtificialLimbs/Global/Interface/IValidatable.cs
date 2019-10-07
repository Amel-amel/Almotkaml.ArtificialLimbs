using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Almotkaml.ArtificialLimbs.Global.Interface
{
    public interface IValidatable
    {
        void Validate(ModelState modelState);
    }
}
