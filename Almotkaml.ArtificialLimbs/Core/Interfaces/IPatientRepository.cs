using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        bool PatientExist(string nationalityNumber = null);
        int AddNewStatues(int PatientID,int DeviceID, Types type);
        string GetPatientName(int id);
        int GetPatientCount();
        int GetMenCount();
        int GetWomenCount();
        int GetChildrenCount();
        int GetNewPatientCount();
    }
}
