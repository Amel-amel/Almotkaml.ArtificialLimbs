using Almotkaml.ArtificialLimbs.Global.Herbler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Branch
    {
        public static Branch New(string name)
        {
            Check.NotEmpty(name, nameof(name));

            var branch = new Branch()
            {
                Name = name,
            };


            return branch;
        }

        private Branch()
        {

        }
        public int BranchId { get; private set; }
        public string Name { get; private set; }
        public ICollection<Place> Places { get; } = new HashSet<Place>();
        public void Modify(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;

        }

    }
}