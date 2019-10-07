using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class AmputationTypes
    {
        [Key]
        public int AmputationTypeID { get; private set; }
        public string Name { get; private set; }
     

        public static AmputationTypes New(string name)
        {
            Check.NotEmpty(name, nameof(name));

            var amputationTypes = new AmputationTypes()
            {
                Name = name,
            };

            return amputationTypes;
        }

        public void Modify(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;
        }
    }
}