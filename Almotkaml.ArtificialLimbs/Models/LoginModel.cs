using Almotkaml.ArtificialLimbs.Core.Resources;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Global.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class LoginModel : ILoginModel
    {

        //[Required(ErrorMessageResourceType = typeof(SharedMessages)
        //            , ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.UserName))]
        public string UserName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(SharedMessages)
        //            , ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Password))]
        public string Password { get; set; }

        //[Range(1990, 3000, ErrorMessageResourceType = typeof(SharedMessages)
        //            , ErrorMessageResourceName = nameof(SharedMessages.TrueYear))]
        //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.Year))]
        //public int Year { get; set; }
        public IEnumerable<int> YearsList { get; set; } = new HashSet<int>();
    }


    //public class ProfileModel : IProfileModel, IValidatable
    //{
    //    public Permission Permissions { get; set; }

    //    //[Required(ErrorMessageResourceType = typeof(SharedMessages)
    //    //            , ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
    //    //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.UserTitle))]
    //    public string Title { get; set; }

    //    //[Required(ErrorMessageResourceType = typeof(SharedMessages)
    //    //            , ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
    //    //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.UserName))]
    //    public string UserName { get; set; }

    //    //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.OldPassword))]
    //    public string OldPassword { get; set; }

    //    //[Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.NewPassword))]
    //    public string NewPassword { get; set; }

    //   // [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.ConfirmPassword))]
    //    public string ConfirmPassword { get; set; }

    //   // [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.ChangePassword))]
    //    public bool ChangePassword { get; set; }
    //    //public void Validate(ModelState modelState)
    //    //{
    //    //    if (!ChangePassword)
    //    //        return;

    //    //    if (string.IsNullOrWhiteSpace(NewPassword))
    //    //        modelState.AddError(
    //    //            m => this.NewPassword,
    //    //            string.Format(SharedMessages.IsRequired, SharedTitles.NewPassword));

    //    //    if (NewPassword != ConfirmPassword)
    //    //        modelState.AddError(m => this.ConfirmPassword, SharedMessages.PasswordNotMatch);
    //    //}
    //}


    public class NotificationModel
    {
        public Category Notifications { get; set; } = new Category();
    }
}