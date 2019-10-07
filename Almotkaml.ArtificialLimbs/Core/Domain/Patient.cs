using Almotkaml.ArtificialLimbs.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }// اسم المريض
        public Gender Gender { get; set; } // الجنس
        public DateTime BirthDate { get; set; } // تاريخ الميلاد
        public string PhoneNumber { get; set; } // رقم الهاتف
        public string NationalityNumber { get; set; } // الرقم الوطني 
        public int NationalityID { get; set; } // الجنسية
        public Nationality Nationality { get; set; } // جدول الجنسيات
        public int CityID { get; private set; } //المدينة
        public City City { get; set; }
        public int? PlaceId { get; private set; } // المحلة
        public Place Place { get; set; }
        public DateTime RegistrationDate { get; set; } // تاريخ التسجيل
        public string Note { get; set; }

        public ICollection<PatientReport> Cities { get; } = new HashSet<PatientReport>();
        public ICollection<DailyStatues> DailyStatues { get; } = new HashSet<DailyStatues>();
        public ICollection<AmputationStatues> AmputationStatues { get; } = new HashSet<AmputationStatues>();

        public static Patient New(string _patientName, Gender _gender, string _birthDate, string _phoneNumber, string _nationalityNO, 
                                    int _NationID,int _cityID,int? _placeID,string _registrationDate, string _note)
        {
            Check.NotEmpty(_patientName, nameof(_patientName));
            Check.NotEmpty(_birthDate, nameof(_birthDate));

            var patient = new Patient()
            {
                PatientName=_patientName,
                Gender  = _gender,
                BirthDate = DateTime.Parse(_birthDate),
                PhoneNumber  = _phoneNumber,
                NationalityNumber = _nationalityNO,
                NationalityID = _NationID,
                CityID = _cityID,
                PlaceId = _placeID,
                RegistrationDate = DateTime.Parse(_registrationDate),
                Note = _note,
            };


            return patient;
        }


        public  void Modify(string _patientName, Gender _gender, string _birthDate, string _phoneNumber, string _nationalityNO,
                                     int _NationID, int _cityID, int? _placeID, string _registrationDate, string _note)
        {
            Check.NotEmpty(_patientName, nameof(_patientName));
            Check.NotEmpty(_birthDate, nameof(_birthDate));

            PatientName = _patientName;
            Gender = _gender;
            BirthDate = DateTime.Parse(_birthDate);
            PhoneNumber = _phoneNumber;
            NationalityNumber = _nationalityNO;
            NationalityID = _NationID;
            CityID = _cityID;
            PlaceId = _placeID;
            RegistrationDate = DateTime.Parse(_registrationDate);
            Note = _note;
          
        }
    }
}