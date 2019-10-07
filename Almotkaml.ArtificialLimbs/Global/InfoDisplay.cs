using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global
{
    public class InfoDisplay
    {
        public string Name { get; private set; }
        public string GroupName { get; private set; }
        public string DefaultGroupName { get; private set; }
        public string ShortName { get; private set; }
        public string Prompt { get; private set; }
        public Type ObjectType { get; set; }

        public PropertyInfo[] GetProperties(Type type)
        {
            ObjectType = type;
            return type.GetProperties();
        }

        public static bool GetValue<TModel>(string propertyName, TModel objectValue)
        {
            try
            {
                return (bool)typeof(TModel).GetProperty(propertyName).GetValue(objectValue, null);
            }
            catch
            {
                return false;
            }
        }


        public void PropertyName(string propertyName)
        {
            try
            {
                var info = ObjectType.GetMember(propertyName);
                var attributes = info[0].GetCustomAttributes(typeof(ShowAttribute), false);
                Name = ((ShowAttribute)attributes[0]).Title;
                GroupName = ((ShowAttribute)attributes[0]).GroupName;
                ShortName = ((ShowAttribute)attributes[0]).Icon;
                Prompt = null;
            }
            catch
            {
                Name = null;
                GroupName = null;
                ShortName = null;
                Prompt = null;
            }
            DefaultGroupName = GroupName ?? "Invalid";
        }
    }
}