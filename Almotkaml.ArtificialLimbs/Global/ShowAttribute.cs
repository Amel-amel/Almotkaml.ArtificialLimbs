using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ShowAttribute : Attribute
    {
        public ShowAttribute()
        {
        }
        public ShowAttribute(string permissionName, string groupName)
        {
            PermissionName = permissionName;
            GroupName = groupName;
        }
        public ShowAttribute(string permissionName, string groupName, string title)
        {
            PermissionName = permissionName;
            GroupName = groupName;
            Title = title;
        }
        public ShowAttribute(string permissionName, string groupName, string title, string icon)
        {
            PermissionName = permissionName;
            GroupName = groupName;
            Title = title;
            Icon = icon;
        }

        public string PermissionName { get; set; }
        public string GroupName { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}