using System;
using System.Collections.Generic;
using Almotkaml.ArtificialLimbs.Global.Herbler;
using Almotkaml.ArtificialLimbs.Core.Domain;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Almotkaml.ArtificialLimbs.Core.Resources;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Country
    {
        public int CountryId { get; private set; }
        //[Required(ErrorMessageResourceType = typeof(SharedMessages),
        //   ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.CountryName))]
        public string Name { get; private set; }
        public ICollection<City> Cities { get; } = new HashSet<City>();

        private Country()
        {

        }

        public static Country New(string name)
        {
            Check.NotEmpty(name, nameof(name));

            var country = new Country()
            {
                Name = name,
            };

            return country;
        }
        
        public void Modify(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;
        }
    }


}