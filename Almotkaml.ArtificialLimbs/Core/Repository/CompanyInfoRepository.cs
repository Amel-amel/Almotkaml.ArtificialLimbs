using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class CompanyInfoRepository : Repository<Infoes>, ICompanyInfoRepository
    {
        
        protected readonly ArtificialLimbsDbContext _Context;
        public CompanyInfoRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }

        public CompanyInfoModel Get()
        {
            var companyInfo = Load();

            if (companyInfo == null)
                throw new Exception("failed to load info !");

            return new CompanyInfoModel()
            {
                ShortName = companyInfo.ShortName,
                LongName = companyInfo.LongName,
                EnglishName = companyInfo.EnglishName,
                Email = companyInfo.Email,
                Address = companyInfo.Address,
                Mobile = companyInfo.Mobile,
                Phone = companyInfo.Phone,
                Website = companyInfo.Website,
                

            };
        }
        public CompanyInfos Load()
        {
            var domainCompanyInfo = new CompanyInfos();

            foreach (var dbCompanyInfo in _Context.Infoes.ToList())
            {
                var type = domainCompanyInfo.GetType().GetProperty(dbCompanyInfo.Name).PropertyType;

                var value = Convert.ChangeType(dbCompanyInfo.Value, type);

                domainCompanyInfo.SetValue(dbCompanyInfo.Name, value);
            }
            return domainCompanyInfo;
        }

        public void Save(CompanyInfos companyInfo)
        {
            var dbCompanyInfos = _Context.Infoes.ToList();

            foreach (var domainCompanyInfoProperty in companyInfo.GetType().GetProperties())
            {
                var dbCompanyInfo = dbCompanyInfos.FirstOrDefault(s => s.Name == domainCompanyInfoProperty.Name);

                var domainValue = domainCompanyInfoProperty.GetValue(companyInfo)?.ToString();

                if (dbCompanyInfo != null)
                    dbCompanyInfo.Value = domainValue;
                else
                    _context.Infoes.Add(new Infoes()
                    {
                        Name = domainCompanyInfoProperty.Name,
                        Value = domainValue
                    });
            }
        }
    }
}