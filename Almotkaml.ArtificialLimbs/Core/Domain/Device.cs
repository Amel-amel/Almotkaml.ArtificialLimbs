using Almotkaml.ArtificialLimbs.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public Department department { get; set; }
        public int DepartmentID { get; set; }
        public Types Type { get; set; }
        
        public static Device New(string _arabicName, string _englishName, int _departmentId,Types _type)
        {
            Check.NotEmpty(_arabicName, nameof(_arabicName));
            Check.MoreThanZero(_departmentId, nameof(_departmentId));
           
            var device = new Device()
            {
                ArabicName = _arabicName ,
                EnglishName = _englishName ,
                DepartmentID = _departmentId ,
                Type =_type,
            };

            return device;
        }

        public void Modify(string _arabicName, string _englishName, int _departmentId, Types _type)
        {
            Check.NotEmpty(_arabicName, nameof(_arabicName));
            Check.MoreThanZero(_departmentId, nameof(_departmentId));

            ArabicName = _arabicName;
            EnglishName = _englishName;
            DepartmentID = _departmentId;
            Type = _type;
        }


    }
}