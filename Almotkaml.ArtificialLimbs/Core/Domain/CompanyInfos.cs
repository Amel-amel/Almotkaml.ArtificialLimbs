using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class CompanyInfos
    {
        public int CompanyInfosID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
 


        public static CompanyInfos New(string _ShortName, string _LongName, string _EnglishName,
                           string _Phone, string _Mobile, string _Email, string _Website,
                           string _Address)
        {
            var companyInfo = new CompanyInfos()
            {
                ShortName = _ShortName,
                LongName = _LongName,
                EnglishName = _EnglishName,
                Mobile = _Mobile,
                Phone = _Phone,
                Email = _Email,
                Website = _Website,
                Address = _Address,
            };


            return companyInfo;
        }

        public void Modify(string _ShortName, string _LongName, string _EnglishName,
                           string _Phone, string _Mobile, string _Email, string _Website,
                           string _Address)
        {
            ShortName = _ShortName;
            LongName = _LongName;
            EnglishName = _EnglishName;
            Mobile = _Mobile;
            Phone = _Phone;
            Email = _Email;
            Website = _Website;
            Address = _Address;
        }
    }
}