using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public static class HtmlRequestHelper
    {
        // Emad : @Html.Controller() يعيد لك اسم الكنترول الذي تقوم بزيارته حالياً
        public static string Controller(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current?.Request.RequestContext?.RouteData?.Values;

            if (routeValues != null && routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        // Emad : @Html.Action() يعيد لك اسم الأكشن الذي تقوم بزيارته حالياً
        public static string Action(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current?.Request.RequestContext?.RouteData?.Values;

            if (routeValues != null && routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }

        // Emad : @Html.ID() يعيد لك قيمة الاي دي الذي تقوم بزيارته حالياً
        public static string Id(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current?.Request.RequestContext?.RouteData?.Values;

            if (routeValues != null && routeValues.ContainsKey("id"))
                return (string)routeValues["id"];

            return string.Empty;
        }

        public static MvcHtmlString Note(this HtmlHelper htmlHelper)
            => htmlHelper.Partial("~/Views/Shared/Note.cshtml");

        public static string Show(this HtmlHelper htmlHelper, bool canEdit) => canEdit ? "تعديل" : "مشاهدة";

        public static MvcHtmlString AjaxNote(this HtmlHelper htmlHelper)
            => htmlHelper.Partial("~/Views/Shared/_AjaxNote.cshtml");

        public static MvcHtmlString Create(this HtmlHelper htmlHelper, bool permission, string text = "إضافة جديد", string actionName = "Create")
        {
            if (permission)
            {
                return htmlHelper.ActionLink(text, actionName, null, new { @class = "btn btn-success" });
            }

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString ButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, string title, string cssClass = null)
        {
            var body = (MemberExpression)expression.Body;

            //Func<TModel, TProperty> method = expression.Compile();
            //try
            //{
            //    TProperty dateValue = method(htmlHelper.ViewData.Model);
            //}
            //catch
            //{
            //}

            string name = body.ToString().Replace("{", "");
            name = name.Replace("}", "");
            string nameToRemove = name.Split('.')[0];

            name = name.Replace(nameToRemove + ".", "");

            var buttonFor = new ButtonFor()
            {
                Title = title,
                Value = value,
                CssClass = cssClass,
                ButtonName = name
            };

            return htmlHelper.Partial("~/Views/Shared/_ButtonFor.cshtml", buttonFor);
        }
        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var body = (MemberExpression)expression.Body;
            string value;

            var method = expression.Compile();
            try
            {
                var dateValue = method(htmlHelper.ViewData.Model);
                value = dateValue.ToString();
            }
            catch
            {
                value = "";
            }

            if (htmlAttributes == null)
            {
                htmlAttributes = new { @class = "field_date form-control", placeholder = "YYYY-MM-DD" };
            }

            string name = body.ToString().Replace("{", "");
            name = name.Replace("}", "");
            string nameToRemove = name.Split('.')[0];

            name = name.Replace(nameToRemove + ".", "");

            var datepick = new DatePick()
            {
                Name = name,
                Value = value,
                Attribute = htmlAttributes,
                IdName = name.Replace('.', '_')
            };

            return htmlHelper.Partial("~/Views/Shared/DatePicker.cshtml", datepick);
        }

        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                htmlAttributes = new { @class = "field_mid form-control", placeholder = "YYYY-MM-DD" };
            }

            var datepick = new DatePick()
            {
                Name = name,
                Value = value,
                Attribute = htmlAttributes,
                IdName = name.Replace('.', '_')
            };


            return htmlHelper.Partial("~/Views/Shared/DatePicker.cshtml", datepick);
        }

        public static ISetting GetSettings(this HtmlHelper htmlHelper)
            => htmlHelper.ViewBag.SettingsModel as ISetting;

        public static dynamic GetPermission(this HtmlHelper htmlHelper)
            => htmlHelper.ViewBag.Permissions as dynamic;

        public static ICompanyInfo GetCompanyInfo(this HtmlHelper htmlHelper)
            => htmlHelper.ViewBag.CompanyInfoModel as ICompanyInfo;

        public static IApplication Application(this HtmlHelper htmlHelper)
            => htmlHelper.ViewBag.ApplicationModel as IApplication;

        public static MvcHtmlString SavedModel(this HtmlHelper htmlHelper, object model)
            => htmlHelper.Partial("~/Views/Shared/_SavedModel.cshtml");

        //public static MvcHtmlString AjaxReloadListFor<TModel, TPropertyFirst, TPropertySecond>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TPropertyFirst>> expressionFirst, Expression<Func<TModel, TPropertySecond>> expressionSecond, string listName, bool permission, string displayName = null, string listNames = null, string ajaxController = null, string controller = null, string action = null, bool stringMenu = false)
        //{
        //    var body1 = (MemberExpression)expressionFirst.Body;

        //    string _name = body1.ToString().Replace("{", "");
        //    _name = _name.Replace("}", "");
        //    string _nameToRemove = _name.Split('.')[0];

        //    _name = _name.Replace(_nameToRemove + ".", "").Replace('.', '_');

        //    AjaxReload ajax = new AjaxReload();
        //    ajax.FirstId = _name;

        //    var body2 = (MemberExpression)expressionSecond.Body;

        //    _name = body2.ToString().Replace("{", "");
        //    _name = _name.Replace("}", "");
        //    _nameToRemove = _name.Split('.')[0];

        //    _name = _name.Replace(_nameToRemove + ".", "").Replace('.', '_');

        //    ajax.SecondId = _name;
        //    ajax.ListName = listName;
        //    ajax.AjaxController = "Ajax";
        //    ajax.ListNames = listName + "s";
        //    ajax.DisplayName = "Name";
        //    ajax.Controller = listName;
        //    ajax.Permission = permission;
        //    ajax.Action = action != null ? action : "Create";
        //    ajax.StringMenu = stringMenu;


        //    if (listNames != null)
        //    {
        //        ajax.ListNames = listNames;
        //    }

        //    if (displayName != null)
        //    {
        //        ajax.DisplayName = displayName;
        //    }
        //    if (ajaxController != null)
        //    {
        //        ajax.AjaxController = ajaxController;
        //    }
        //    if (controller != null)
        //    {
        //        ajax.Controller = controller;
        //    }

        //    return PartialExtensions.Partial(htmlHelper, "~/Views/Shared/AjaxReloadList.cshtml", ajax);
        //}

        //public static MvcHtmlString AjaxReloadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string listName, bool permission, string displayName = null, string listNames = null, string ajaxController = null, string controller = null, string action = null, bool stringMenu = false)
        //{
        //    var body1 = (MemberExpression)expression.Body;

        //    string _name = body1.ToString().Replace("{", "");
        //    _name = _name.Replace("}", "");
        //    string _nameToRemove = _name.Split('.')[0];

        //    _name = _name.Replace(_nameToRemove + ".", "").Replace('.', '_');

        //    AjaxReload ajax = new AjaxReload();
        //    ajax.FirstId = _name;

        //    ajax.SecondId = null;
        //    ajax.ListName = listName;
        //    ajax.AjaxController = "Ajax";
        //    ajax.ListNames = listName + "s";
        //    ajax.DisplayName = "Name";
        //    ajax.Controller = listName;
        //    ajax.Permission = permission;
        //    ajax.Action = action != null ? action : "Create";
        //    ajax.StringMenu = stringMenu;


        //    if (listNames != null)
        //    {
        //        ajax.ListNames = listNames;
        //    }

        //    if (displayName != null)
        //    {
        //        ajax.DisplayName = displayName;
        //    }
        //    if (ajaxController != null)
        //    {
        //        ajax.AjaxController = ajaxController;
        //    }
        //    if (controller != null)
        //    {
        //        ajax.Controller = controller;
        //    }

        //    return PartialExtensions.Partial(htmlHelper, "~/Views/Shared/AjaxReload.cshtml", ajax);
        //}
    }
}