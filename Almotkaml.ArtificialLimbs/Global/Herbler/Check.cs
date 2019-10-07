using Almotkaml.ArtificialLimbs.Global.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public static class Check
    {
        public static void NotNull(object obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void IsValidDate(DateTime date, string parameterName)
        {
            if (!date.IsValid())
                throw new ArgumentOutOfRangeException(parameterName, "Date cannot be before 100 years or more than 100");
        }

        public static void NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("String cannot be empty", parameterName);
        }

        public static void MoreThanZero(decimal number, string parameterName)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(parameterName, "Number must be more than zero");
        }
        public static void ZeroOrMore(decimal number, string parameterName)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(parameterName, "Number cannot be less than zero");
        }
    }
}