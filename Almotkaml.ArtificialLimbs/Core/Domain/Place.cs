using Almotkaml.ArtificialLimbs.Global.Herbler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Place
    {

        public static Place New(string name, int BranchId)
        {
            Check.NotEmpty(name, nameof(name));
            Check.MoreThanZero(BranchId, nameof(BranchId));


            var place = new Place()
            {
                Name = name,
                BranchId = BranchId,
            };


            return place;
        }
        public static Place New(string name, Branch branch)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(branch, nameof(branch));


            var place = new Place()
            {
                Name = name,
                Branch = branch
            };


            return place;
        }
        private Place()
        {

        }
        public int PlaceId { get; private set; }
        public string Name { get; private set; }
        public Branch Branch { get; private set; }
        public int BranchId { get; private set; }
        //public ICollection<Employee> Employees { get; } = new HashSet<Employee>();
        public void Modify(string name, int BranchId)
        {
            Check.NotEmpty(name, nameof(name));
            Check.MoreThanZero(BranchId, nameof(BranchId));

            Name = name;
            BranchId = BranchId;
            Branch = null;

        }

        public void Modify(string name, Branch branch)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(branch, nameof(branch));

            Name = name;
            Branch = branch;

        }

    }
}