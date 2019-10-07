using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class DeleteDialog
    {
        public DeleteDialog()
        {
            Name = "delete";
            Value = "Id";
        }

        public DeleteDialog(object value, string fieldName)
        {
            Name = fieldName;
            Value = value;
        }

        public DeleteDialog(string propertyName, object value)
        {
            Name = "delete" + propertyName;
            Value = value;
        }
        public string Name { get; private set; }
        public object Value { get; private set; }
    }
}