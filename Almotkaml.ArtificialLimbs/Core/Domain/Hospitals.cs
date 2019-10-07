using Almotkaml.ArtificialLimbs.Global.Herbler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Hospitals
    {
        public int HospitalsId { get; private set; }
        public string Name { get; private set; }

        public static Hospitals New(string name)
        {
            Check.NotEmpty(name, nameof(name));

            var hospital = new Hospitals()
            {
                Name = name,
            };

            return hospital;
        }

        public void Modify(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;
        }
    }
}