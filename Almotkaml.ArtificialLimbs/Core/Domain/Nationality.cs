using Almotkaml.ArtificialLimbs.Global.Herbler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Nationality
    {

        private Nationality()
        {

        }
        public int NationalityId { get; private set; }
        public string Name { get; private set; }

        //public ICollection<Employee> Employees { get; } = new HashSet<Employee>();

        public static Nationality New(string name)
        {
            Check.NotEmpty(name, nameof(name));

            var nationality = new Nationality()
            {
                Name = name,
            };


            return nationality;
        }

        public void Modify(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;

        }
    }
}