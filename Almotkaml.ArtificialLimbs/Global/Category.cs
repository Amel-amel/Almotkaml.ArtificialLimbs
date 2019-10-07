using Almotkaml.Extensions;
using Almotkaml.ArtificialLimbs.Controllers;
using Almotkaml.ArtificialLimbs.Core;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Global.Herbler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace Almotkaml.ArtificialLimbs.Global
{

    public class BaseCategory
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public ICollection<Categry> Categories { get; } = new HashSet<Categry>();
    }

    public class Categry
    {
        private string _controllerName;
        private string _actionName;

        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value.Replace("Controller", ""); }
        }

        public string ActoinName
        {
            get { return _actionName; }
            set { _actionName = value.Replace("Action", ""); }
        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<string> AddedPermissions { get; } = new HashSet<string>();


    }

    public class AllTrue
    {
        public AllTrue()
        {
            _areTrue = true;
        }

        private bool _areTrue;

        public bool AreTrue
        {
            get { return _areTrue; }
            set
            {
                if (_areTrue)
                    _areTrue = value;
            }
        }
    }

    public class Category
    {
     

        private ICollection<BaseCategory> Categories { get; set; }

        public static string GetPermissionPhrase(string name, Permission permission)
        {
            var secondName = name.Contains("_") ? name.Split('_')[1] : name;

            switch (secondName)
            {
                case "Create":
                    return "إضافة";
                case "Edit":
                    return "تعديل";
                case "Delete":
                    return "حذف";
            }

            var resxName = permission.GetAttribute<DisplayAttribute>(name)?.Name;

            return resxName != null
                ? new ResourceManager(typeof(Resources.SharedTitles)).GetString(resxName)
                : secondName;
        }

        public ICollection<BaseCategory> GetCategories(HtmlHelper htmlHelper)
        {

            if (Categories != null)
                return Categories;

            var permissions =  htmlHelper.GetPermission();

            //if (permissions == null)
            //    throw new Exception("No permissions loaded");

            //var settings = htmlHelper.GetSettings();

            //if (settings == null)
            //    throw new Exception("No settings loaded");

            Categories = new HashSet<BaseCategory>()
            {
                   new BaseCategory()
                    {
                        Title = "الإعدادات العامة",
                        Icon = "icon-cog",
                        Categories =
                        {
                            //new Categry()
                            //{
                            //    Title = "إعدادات المنظومة",
                            //    ControllerName = nameof(SettingController),
                            //    ActoinName = nameof(SettingController.Index),
                            //    Icon = "fa fa-cog",
                            //    //IsVisible = permissions.Setting,
                            //},
                            new Categry()
                            {
                                Title = "بيانات الجهة",
                                ControllerName = nameof(CompanyInfoController),
                                ActoinName = nameof(CompanyInfoController.Index),
                                Icon = "fa fa-info",
                                IsVisible = permissions.CompanyInfo,
                            },
                            new Categry()
                            {
                                Title = "صلاحيات المستخدمين",
                                ControllerName = nameof(UserGroupController),
                                ActoinName = nameof(UserGroupController.Index),
                                Icon = "fa fa-users",
                                IsVisible = permissions.UserGroup,
                            },
                            new Categry()
                            {
                                Title = "إدارة المستخدمين",
                                ControllerName = nameof(UserController),
                                ActoinName = nameof(UserController.Index),
                                Icon = "fa fa-user",
                                IsVisible = permissions.User,
                            },
                            new Categry()
                            {
                                Title = "مراقبة المستخدمين",
                                ControllerName = nameof(ActivityController),
                                ActoinName = nameof(ActivityController.Index),
                                Icon = " icon-monitor",
                                IsVisible = permissions.Activity,
                            },
                            new Categry()
                            {
                                Title = "النسخ الاحتياطي",
                                ControllerName = nameof(BackUpController),
                                ActoinName = nameof(BackUpController.Index),
                                Icon = "fa fa-database",
                                IsVisible = permissions.BackUp,// || permissions.Restore,
                                //AddedPermissions = {nameof(permissions.BackUp), nameof(permissions.Restore)}
                            },
                        },
                    },
                    new BaseCategory()
                    {
                        Title = "الإعدادات الأساسية",
                        Icon = "icon-share",
                        Categories =
                        {
                            new Categry()
                            {
                                Title = "الجنسيات",
                                ControllerName = nameof(NationalityController),
                                ActoinName = nameof(NationalityController.Index),
                                Icon = "icon-vcard",
                                IsVisible = permissions.Nationality,
                            },
                            new Categry()
                            {
                                Title = "البلدان",
                                ControllerName = nameof(CountriesController),
                                ActoinName = nameof(CountriesController.Index),
                                Icon = "fa fa-globe",
                                IsVisible = permissions.Countries,
                            },
                            new Categry()
                            {
                                Title = "المدن",
                                ControllerName = nameof(CitiesController),
                                ActoinName = nameof(CitiesController.Index),
                                Icon = "fa fa-building-o",
                                IsVisible = permissions.Cities,
                            },
                            new Categry()
                            {
                                Title = "الفرع البلدي",
                                ControllerName = nameof(BranchController),
                                ActoinName = nameof(BranchController.Index),
                                Icon = "fa fa-object-group",
                                IsVisible = permissions.Branch,
                            },
                            new Categry()
                            {
                                Title = "المحلة / الشارع",
                                ControllerName = nameof(PlaceController),
                                ActoinName = nameof(PlaceController.Index),
                                Icon = " icon-location",
                                IsVisible = permissions.Place,
                            },
                            new Categry()
                            {
                                Title = "الجهات",
                                ControllerName = nameof(HospitalsController),
                                ActoinName = nameof(HospitalsController.Index),
                                Icon = "fa fa-hospital-o",
                                IsVisible = permissions.Hospitals,
                            },
                              new Categry()
                            {
                                Title = "أنواع البتر",
                                ControllerName = nameof(AmputationTypesController),
                                ActoinName = nameof(AmputationTypesController.Index),
                                Icon = "fa fa-plus-square",
                                IsVisible = permissions.AmputationTypes,
                            },
                              new Categry()
                            {
                                Title = "الأقسام",
                                ControllerName = nameof(DepartmentController),
                                ActoinName = nameof(DepartmentController.Index),
                                Icon = "fa fa-cubes",
                                IsVisible = permissions.Department,
                            },
                              new Categry()
                            {
                                Title = "الفنيين",
                                ControllerName = nameof(EmployeeController),
                                ActoinName = nameof(EmployeeController.Index),
                                Icon = "fa fa-user-md",
                                IsVisible = permissions.Employee,
                            },
                              new Categry()
                            {
                                Title = "الأجهزة/الأطراف التعويضية",
                                ControllerName = nameof(DeviceController),
                                ActoinName = nameof(DeviceController.Index),
                                Icon = "fa fa-plus",
                                IsVisible = permissions.Device,
                            },
                                  
                        },
                    },

                    new BaseCategory()
                    {
                        Title = "العمليات",
                        Icon = "icon-tools",
                        Categories =
                        {
                            new Categry()
                            {
                                Title = "المرضى",
                                ControllerName = nameof(PatientController),
                                ActoinName = nameof(PatientController.Index),
                                Icon = "fa fa-wheelchair",
                                IsVisible = permissions.Patient,
                            },
                            new Categry()
                            {
                                Title = "حالات يومية",
                                ControllerName = nameof(DailyStatuesController),
                                ActoinName = nameof(DailyStatuesController.Index),
                                Icon = "fa fa-calendar-plus-o",
                                IsVisible = permissions.DailyStatues,
                            },
                            new Categry()
                            {
                                Title = "حالات البتر",
                                ControllerName = nameof(AmputationStatuesController),
                                ActoinName = nameof(AmputationStatuesController.Index),
                                Icon = "fa fa-user-plus",
                                IsVisible = permissions.AmputationStatues,
                            },
                            new Categry()
                            {
                                Title = "تفاصيل حالات يومية",
                                ControllerName = nameof(DailyDetailesController),
                                ActoinName = nameof(DailyDetailesController.Index),
                                Icon = "fa fa-user-plus",
                                IsVisible = false,
                            },
                            new Categry()
                            {
                                Title = "تفاصيل حالات البتر",
                                ControllerName = nameof(AmputationDetailsController),
                                ActoinName = nameof(AmputationDetailsController.Index),
                                Icon = "fa fa-user-plus",
                                IsVisible = false,
                            },
                        },
                    },
                    new BaseCategory()
                    {
                        Title = "التقارير",
                        Icon = " icon-doc-text",
                        Categories =
                        {
                            new Categry()
                            {
                                Title = "تقرير الحالات اليومية",
                                 ControllerName = nameof(ReportsController),
                                ActoinName = nameof(ReportsController.DailyReport),
                                Icon = "icon-doc-text",
                                IsVisible = permissions.Reports,
                            },
                            new Categry()
                            {
                                Title = "تقرير حالات البـتـر",
                                 ControllerName = nameof(ReportsController),
                                ActoinName = nameof(ReportsController.AmputationReport),
                                Icon = "fa fa-file-text",
                                IsVisible = permissions.Reports,
                            },
                          

                            new Categry()
                            {
                                Title = "التقرير الحـر",
                                ControllerName = nameof(DynamicReportController),
                                ActoinName = nameof(DynamicReportController.Index),
                                Icon = " icon-doc-text-inv",
                                IsVisible = permissions.DynamicReport,
                            },

                        },
                    },
                        new BaseCategory()
                    {
                        Title = "الإحصائيات",
                        Icon = "icon-chart-bar",
                        Categories =
                        {
                             new Categry()
                            {
                                Title = "إحصائية حالات يومية",
                                 ControllerName = nameof(StatisticsReportController),
                                ActoinName = nameof(StatisticsReportController.DStatisticsReport),
                                Icon = "fa fa-bar-chart",
                                IsVisible = permissions.StatisticsReport,
                            },
                             new Categry()
                            {
                                Title = "إحصائية حالات بـتـر",
                                 ControllerName = nameof(StatisticsReportController),
                                ActoinName = nameof(StatisticsReportController.AStatisticsReport),
                                Icon = "fa fa-pie-chart",
                                IsVisible = permissions.StatisticsReport,
                            },
                             new Categry()
                            {
                                Title = "إحصائية خدمات الورشة",
                                 ControllerName = nameof(AchievementsController),
                                ActoinName = nameof(AchievementsController.Index),
                                Icon = "fa fa-area-chart",
                                IsVisible = permissions.Achievements,
                            },
                        },
                    },





            };

            return Categories;
        }

           }
}