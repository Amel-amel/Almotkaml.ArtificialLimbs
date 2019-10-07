using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
       
        public CountryRepository(ArtificialLimbsDbContext context)
            : base(context)
        {

        }


        public CountryModel Select(int id)
        {
            //if (!HavePermission(ApplicationUser.Permissions.Country_Create))
            //    return Null<CountryModel>(RequestState.NoPermission);

            return new CountryModel()
            {

                //CanCreate = ApplicationUser.Permissions.Country_Create,
                //CanEdit = ApplicationUser.Permissions.Country_Edit,
                //CanDelete = ApplicationUser.Permissions.Country_Delete,
                //CountryList = _unitOfWork.Country.Find(id).First(),

            };
        }
    }
}