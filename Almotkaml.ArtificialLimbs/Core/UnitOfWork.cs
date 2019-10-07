using Almotkaml.Extensions;
using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Core.Resources;
using Almotkaml.ArtificialLimbs.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Web;
using System.Web.UI.WebControls;
using Almotkaml.ArtificialLimbs.Core.Repository;
using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.EntityCore.Resource;

namespace Almotkaml.ArtificialLimbs.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ArtificialLimbsDbContext _context;
        public string Message { get; private set; }
        public IUserGroupRepository _UserGroup { get; private set; }
        public IUserRepository _User { get; private set; }
        public IActivityRepository _activities { get; private set; }
        public ICompanyInfoRepository _CompanyInfos { get; private set; }
        public ICityRepository _City { get; private set; }
        public ICountryRepository _Country { get; private set; }
        public IBranchRepository _Branch { get; private set; }
        public INationalityRepository _Nationality { get; private set; }
        public IPlaceRepository _Place { get; private set; }
        public IHospitalsRepository _Hospitals { get; private set; }
        public IAmputationTypesRepository _AmputationTypes { get; private set; }
        public IDepartmentRepository _Department { get; private set; }
        public IEmployeeRepository _Employee { get; private set; }
        public IDeviceRepository _Device { get; private set; }
        public IPatientRepository _Patient { get; private set; }
        public IDailyDetailsRepository _DailyDetails { get; private set; }
        public IDailyStatuesRepository _DailyStatues { get; private set; }
        public IAmputationDetailsRepository _AmputationDetails { get; private set; }
        public IAmputationStatuesRepository _AmputationStatues { get; private set; }
        public IAmputationReasonRepository _AmputationReason { get; private set; }
        public IReportsRepository _Reports { get; private set; }
        public IStatisticsReportRepository _StatisticsReport { get; private set; }
        public IDynamicReportRepository _DynamicReport { get; private set; }
        public IAchievementsRepository _AchievementsReport { get; private set; }
        public UnitOfWork(ArtificialLimbsDbContext context)
        {
            _context = context;

            if (_context.Database.CreateIfNotExists())
                SeedData.Load(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Complete(Expression<Func<Permission, bool>> expression)
        {
            _context.LoggedInUserId = (int)HttpContext.Current.Session["UserID"];
            if (expression != null)
                _context.Activities.Add(Activity.New(_context.LoggedInUserId, expression.ToExpressionTarget()));

            Complete();
        }

        public bool TryComplete()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Message = e.ToString();

                if (e.InnerException?.Message.Contains("table \"dbo.") ?? false)
                {
                    var tableName = e.InnerException.Message.Between("table \"dbo.", '"');
                    var name = new ResourceManager(typeof(Tables)).GetString(tableName);
                    Message = SharedMessages.UnableToDelete + " "
                              + (string.IsNullOrWhiteSpace(name) ? tableName : name);
                }
                if (e.Message.Contains("' and '"))
                {
                    var tableName = e.Message.Between("' and '", '\'');
                    var name = new ResourceManager(typeof(Tables)).GetString(tableName);
                    Message = SharedMessages.UnableToDelete + " "
                              + (string.IsNullOrWhiteSpace(name) ? tableName : name);
                }
                return false;
            }

            return true;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
        public bool BackUp(string path)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            var cn = new SqlConnection(connectionString);
            var dbname = cn.Database;
            if (cn.State != ConnectionState.Open)
                cn.Open();

            if (string.IsNullOrWhiteSpace(path))
            {
                const string folder = @"D:\" + nameof(ArtificialLimbs) + "BackUp";

                Directory.CreateDirectory(folder);

                path = folder + @"\A" + DateTime.Now.ToString("yyMMddHHmmss") + ".bak";
            }

            var cmd = new SqlCommand
            {
                CommandText = @"BACKUP DATABASE " + dbname
                              + @" TO DISK = '" + path + "'",
                CommandType = CommandType.Text,
                Connection = cn
            };

            cmd.ExecuteReader();
            cn.Close();
            return true;
        }

        public IUserGroupRepository UserGroup
        {
            get
            {
                if (_UserGroup != null)
                    return _UserGroup;

                _UserGroup = new UserGroupRepository(_context);
                return _UserGroup;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_User != null)
                    return _User;

                _User = new UserRepository(_context);
                return _User;
            }
        }
        public IActivityRepository Activities
        {
            get
            {
                if (_activities != null)
                    return _activities;

                _activities = new ActivityRepository(_context);
                return _activities;
            }
        }
        public ICompanyInfoRepository CompanyInfos
        {
            get
            {
                if (_CompanyInfos != null)
                    return _CompanyInfos;

                _CompanyInfos = new CompanyInfoRepository(_context);
                return _CompanyInfos;
            }
        }
        public ICityRepository City
        {
            get
            {
                if (_City != null)
                    return _City;

                _City = new CityRepository(_context);
                return _City;
            }
        }
        public ICountryRepository Country
        {
            get
            {
                if (_Country != null)
                    return _Country;

                _Country = new CountryRepository(_context);
                return _Country;
            }
        }
        public IBranchRepository Branch
        {
            get
            {
                if (_Branch != null)
                    return _Branch;

                _Branch = new BranchRepository(_context);
                return _Branch;
            }
        }
        public INationalityRepository Nationality
        {
            get
            {
                if (_Nationality != null)
                    return _Nationality;

                _Nationality = new NationalityRepository(_context);
                return _Nationality;
            }
        }
        public IPlaceRepository Place
        {
            get
            {
                if (_Place != null)
                    return _Place;

                _Place = new PlaceRepository(_context);
                return _Place;
            }
        }
        public IHospitalsRepository Hospitals
        {
            get
            {
                if (_Hospitals != null)
                    return _Hospitals;

                _Hospitals = new HospitalsRepository(_context);
                return _Hospitals;
            }
        }
        public IAmputationTypesRepository AmputationTypes
        {
            get
            {
                if (_AmputationTypes != null)
                    return _AmputationTypes;

                _AmputationTypes = new AmputationTypesRepository(_context);
                return _AmputationTypes;
            }
        }

        public IDepartmentRepository Department
        {
            get
            {
                if (_Department != null)
                    return _Department;

                _Department = new DepartmentRepository(_context);
                return _Department;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_Employee != null)
                    return _Employee;

                _Employee = new EmployeeRepository(_context);
                return _Employee;
            }
        }

        public IDeviceRepository  Device
        {
            get
            {
                if (_Device != null)
                    return _Device;

                _Device = new DeviceRepository(_context);
                return _Device;
            }
        }

        public IPatientRepository Patient
        {
            get
            {
                if (_Patient != null)
                    return _Patient;

                _Patient = new PatientRepository(_context);
                return _Patient;
            }
        }

        public IDailyStatuesRepository DailyStatues
        {
            get
            {
                if (_DailyStatues != null)
                    return _DailyStatues;

                _DailyStatues = new DailyStatuesRepository(_context);
                return _DailyStatues;
            }
        }

        public IDailyDetailsRepository DailyDetails
        {
            get
            {
                if (_DailyDetails != null)
                    return _DailyDetails;

                _DailyDetails = new DailyDetailsRepository(_context);
                return _DailyDetails;
            }
        }
        public IAmputationStatuesRepository AmputationStatues
        {
            get
            {
                if (_AmputationStatues != null)
                    return _AmputationStatues;

                _AmputationStatues = new AmputationStatuesRepository(_context);
                return _AmputationStatues;
            }
        }

        public IAmputationDetailsRepository AmputationDetails
        {
            get
            {
                if (_AmputationDetails != null)
                    return _AmputationDetails;

                _AmputationDetails = new AmputationDetailsRepository(_context);
                return _AmputationDetails;
            }
        }

        public IAmputationReasonRepository AmputationReason
        {
            get
            {
                if (_AmputationReason != null)
                    return _AmputationReason;

                _AmputationReason = new AmputationReasonRepository(_context);
                return _AmputationReason;
            }
        }

        
        public IReportsRepository Reports
        {
            get
            {
                if (_Reports != null)
                    return _Reports;

                _Reports = new ReportsRepository(_context);
                return _Reports;
            }
        }
        public IStatisticsReportRepository StatisticsReport
        {
            get
            {
                if (_StatisticsReport != null)
                    return _StatisticsReport;

                _StatisticsReport = new StatisticsReportRepository(_context);
                return _StatisticsReport;
            }
        }

        public IDynamicReportRepository DynamicReport
        {
            get
            {
                if (_DynamicReport != null)
                    return _DynamicReport;

                _DynamicReport = new DynamicReportRepository(_context);
                return _DynamicReport;
            }
        }

        public IAchievementsRepository AchievementsReport
        {
            get
            {
                if (_AchievementsReport != null)
                    return _AchievementsReport;

                _AchievementsReport = new AchievementsRepository(_context);
                return _AchievementsReport;
            }
        }

    }
}