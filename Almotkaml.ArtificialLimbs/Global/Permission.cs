using Almotkaml.Attributes;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global
{
    public class Permission : Category
    {
        public bool CompanyInfo { get; set; }
        [Phrase(typeof(Notifications), nameof(CompanyInfo_Edit))]
        public bool CompanyInfo_Edit { get; set; }

        public bool UserGroup { get; set; }
        [Phrase(typeof(Notifications), nameof(UserGroup_Create))]
        public bool UserGroup_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(UserGroup_Edit))]
        public bool UserGroup_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(UserGroup_Delete))]
        public bool UserGroup_Delete { get; set; }

        public bool User { get; set; }
        [Phrase(typeof(Notifications), nameof(User_Create))]
        public bool User_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(User_Edit))]
        public bool User_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(User_Delete))]
        public bool User_Delete { get; set; }

        public bool Activity { get; set; }
        [Phrase(typeof(Notifications), nameof(UserActivity_Create))]
        public bool UserActivity_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(UserActivity_Edit))]
        public bool UserActivity_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(UserActivity_Delete))]
        public bool UserActivity_Delete { get; set; }

        public bool BackUp { get; set; }
        [Phrase(typeof(Notifications), nameof(BackUp_Create))]
        public bool BackUp_Create { get; set; }
        public bool BackUp_Edit { get; set; }

        public bool Cities { get; set; }
        [Phrase(typeof(Notifications), nameof(Cities_Create))]
        public bool Cities_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Cities_Edit))]
        public bool Cities_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Cities_Delete))]
        public bool Cities_Delete { get; set; }

        public bool Countries { get; set; }
        [Phrase(typeof(Notifications), nameof(Countries_Create))]
        public bool Countries_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Countries_Create))]
        public bool Countries_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Countries_Create))]
        public bool Countries_Delete { get; set; }

        public bool Nationality { get; set; }
        [Phrase(typeof(Notifications), nameof(Nationality_Create))]
        public bool Nationality_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Nationality_Edit))]
        public bool Nationality_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Nationality_Delete))]
        public bool Nationality_Delete { get; set; }

        public bool Branch { get; set; }
        [Phrase(typeof(Notifications), nameof(Branch_Create))]
        public bool Branch_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Branch_Edit))]
        public bool Branch_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Branch_Delete))]
        public bool Branch_Delete { get; set; }

        public bool Place { get; set; }
        [Phrase(typeof(Notifications), nameof(Place_Create))]
        public bool Place_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Place_Edit))]
        public bool Place_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Place_Delete))]
        public bool Place_Delete { get; set; }

        public bool Hospitals { get; set; }
        [Phrase(typeof(Notifications), nameof(Hospitals_Create))]
        public bool Hospitals_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Hospitals_Edit))]
        public bool Hospitals_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Hospitals_Delete))]
        public bool Hospitals_Delete { get; set; }

        public bool AmputationTypes { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationTypes_Create))]
        public bool AmputationTypes_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationTypes_Edit))]
        public bool AmputationTypes_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationTypes_Delete))]
        public bool AmputationTypes_Delete { get; set; }

        public bool Department { get; set; }
        [Phrase(typeof(Notifications), nameof(Department_Create))]
        public bool Department_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Department_Edit))]
        public bool Department_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Department_Delete))]
        public bool Department_Delete { get; set; }

        public bool Employee { get; set; }
        [Phrase(typeof(Notifications), nameof(Employee_Create))]
        public bool Employee_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Employee_Edit))]
        public bool Employee_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Employee_Delete))]
        public bool Employee_Delete { get; set; }

        public bool Device { get; set; }
        [Phrase(typeof(Notifications), nameof(Device_Create))]
        public bool Device_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Device_Edit))]
        public bool Device_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Device_Delete))]
        public bool Device_Delete { get; set; }

        public bool Patient { get; set; }
        [Phrase(typeof(Notifications), nameof(Patient_Create))]
        public bool Patient_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(Patient_Edit))]
        public bool Patient_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(Patient_Delete))]
        public bool Patient_Delete { get; set; }

        public bool DailyStatues { get; set; }
        [Phrase(typeof(Notifications), nameof(DailyStatues_Create))]
        public bool DailyStatues_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(DailyStatues_Edit))]
        public bool DailyStatues_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(DailyStatues_Delete))]
        public bool DailyStatues_Delete { get; set; }

        public bool AmputationStatues { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationStatues_Create))]
        public bool AmputationStatues_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationStatues_Edit))]
        public bool AmputationStatues_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationStatues_Delete))]
        public bool AmputationStatues_Delete { get; set; }

        public bool DailyDetailes { get; set; }
        [Phrase(typeof(Notifications), nameof(DailyDetailes_Create))]
        public bool DailyDetailes_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(DailyDetailes_Edit))]
        public bool DailyDetailes_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(DailyDetailes_Delete))]
        public bool DailyDetailes_Delete { get; set; }

        public bool AmputationDetails { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationDetails_Create))]
        public bool AmputationDetails_Create { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationDetails_Edit))]
        public bool AmputationDetails_Edit { get; set; }
        [Phrase(typeof(Notifications), nameof(AmputationDetails_Delete))]
        public bool AmputationDetails_Delete { get; set; }

        public bool DynamicReport { get; set; }
        [Phrase(typeof(Notifications), nameof(DynamicReport_Create))]
        public bool DynamicReport_Create { get; set; }

        public bool StatuesReport { get; set; }
        public bool Reports { get; set; }
        public bool StatisticsReport { get; set; }
        public bool Achievements { get; set; }
    }
}