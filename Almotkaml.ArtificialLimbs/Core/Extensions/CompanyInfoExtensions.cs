using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class CompanyInfoExtensions
    {


        public static IEnumerable<CompanyInfoGridRow> ToGrid(this IEnumerable<CompanyInfos> CompanyInfo)
            => CompanyInfo.Select(d => new CompanyInfoGridRow()
            {
                CompanyInfosID = d.CompanyInfosID,
                ShortName = d.ShortName,
                LongName = d.LongName,
                EnglishName = d.EnglishName,
                Phone = d.Phone,
                Mobile = d.Mobile,
                Email = d.Email,
                Website = d.Website,
                Address = d.Address

            });
    }
}