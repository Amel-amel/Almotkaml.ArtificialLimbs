using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class PatientRepository : Repository<Patient >, IPatientRepository
    {
        private readonly ArtificialLimbsDbContext _Context;
        private readonly UnitOfWork _UnitOfWork;
        int year = DateTime.Now.Year;
        public PatientRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = new ArtificialLimbsDbContext();
            _UnitOfWork = new UnitOfWork(_context);
        }

        public bool PatientExist(string nationalityNumber = null)
        {
            return _context.Patients.Any(d => d.NationalityNumber.ToString() == nationalityNumber);
        }

        public string GetPatientName(int id)
        {
            return _context.Patients.Find (id).PatientName;
        }

        public int AddNewStatues(int PatientID, int DeviceID, Types type)
        {
            if (type == Types.Daily)
            {
                var dailystatues = DailyStatues.New(PatientID, DeviceID);
                _UnitOfWork.DailyStatues.Add(dailystatues);
                _UnitOfWork.Complete(p => p.DailyStatues_Create);
                return dailystatues.DailyStatuesID;
            }


            return 0;
        }

        public int GetPatientCount()
        {
            return _UnitOfWork.Patient.GetAll().Where(p => p.RegistrationDate.Year == year).Count();
        }

        public int GetMenCount()
        {
            return _UnitOfWork.Patient.GetAll().Where(p => p.Gender == Core.Enum.Gender.male 
                                                        && p.BirthDate.Year <= year - 12 
                                                        && p.RegistrationDate.Year == year).Count();
        }

        public int GetWomenCount()
        {
            return _UnitOfWork.Patient.GetAll().Where(p => p.Gender == Core.Enum.Gender.female 
                                                        && p.BirthDate.Year <= year - 12
                                                        && p.RegistrationDate.Year == year).Count();
        }

        public int GetChildrenCount()
        {
            return _UnitOfWork.Patient.GetAll().Where(p => p.BirthDate.Year >= year - 12
                                                        && p.RegistrationDate.Year == year).Count();
        }

        public int GetNewPatientCount()
        {
            return _UnitOfWork.AmputationStatues.GetAll().Where(a => a.ReceiptDate == null).Count()
                                + _UnitOfWork.DailyStatues.GetAll().Where(a => a.ReceiptDate == null).Count();
        }
    }
}