using Almotkaml.ArtificialLimbs.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this AmputationPart enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        public static string GetDescription(this AmputationPart enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Description : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }
    }
}