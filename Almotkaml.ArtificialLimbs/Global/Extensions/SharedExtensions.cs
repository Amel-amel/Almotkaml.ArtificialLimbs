using Almotkaml.ArtificialLimbs.Global.Herbler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

namespace Almotkaml.ArtificialLimbs.Global.Extensions
{
    public static class SharedExtensions
    {
        public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
            => enumerable as IList<T> ?? enumerable.ToList();

        public static T As<T>(this object obj) where T : class
            => obj as T ?? obj.ToSerializedObject().ToDeserializedObject<T>();

        public static void SetValue(this object obj, string propertyName, object newValue)
            => obj.GetType()?.GetRuntimeProperty(propertyName)?.SetValue(obj, newValue);
        public static void SetValue<TSource>(this TSource obj, Expression<Func<TSource, object>> expression, object newValue)
            => obj.GetType()?.GetRuntimeProperty(expression.ToExpressionTarget())?.SetValue(obj, newValue);

        public static string ToExpressionTarget<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
            => ExpressionManager.GetFullPropertyName(expression);

        private static string GetNameFromNamedArgument(IList<CustomAttributeNamedArgument> namedArguments)
        {
            if (namedArguments == null)
                return string.Empty;

            var name = namedArguments.FirstOrDefault(q => q.MemberName == "Name").TypedValue.Value;
            var resourceType = namedArguments.FirstOrDefault(q => q.MemberName == "ResourceType").TypedValue.Value;

            if (resourceType == null)
                return name?.ToString();

            return name == null
                ? null
                : new ResourceManager((Type)resourceType).GetString(name.ToString());
        }

        public static string DisplayFieldName(this Type type, string fieldName)
            => GetNameFromNamedArgument(type.GetRuntimeField(fieldName)
            ?.CustomAttributes
            .FirstOrDefault()
            ?.NamedArguments);

        public static string DisplayPropertyName(this Type type, string propertyName)
            => GetNameFromNamedArgument(type.GetRuntimeProperty(propertyName)
            ?.CustomAttributes
            .FirstOrDefault()
            ?.NamedArguments);
        public static string ToYearlyNumber(this long number, DateTime date)
            => date.Year + "-" + number;
        public static string ToYearlyNumber(this long number, string date)
            => date.ToDateTime().Year + "-" + number;
        public static string ToYearlyNumber(this int number, DateTime date)
                    => date.Year + "-" + number;
        public static string ToYearlyNumber(this int number, string date)
            => date.ToDateTime().Year + "-" + number;

        public static long ToPureNumber(this string number)
        {
            if (number == null)
                return 0;

            var insideNumber = number.Contains("-") ? number.Split('-')[1] : number;

            long newNumber;
            long.TryParse(insideNumber.Trim(), out newNumber);
            return newNumber;
        }

        public static TAttribute GetAttribute<TAttribute>(this object objectReference, string propertyName)
            where TAttribute : Attribute => objectReference
                .GetType()
                ?.GetRuntimeProperty(propertyName)
                ?.GetCustomAttribute<TAttribute>();


        public static TAttribute GetAttribute<TAttribute>(this Type objectType, string propertyName)
            where TAttribute : Attribute => objectType
                ?.GetRuntimeProperty(propertyName)
                ?.GetCustomAttribute<TAttribute>();


        public static string Between(this string value, string before, char after)
        {
            var allAfter = value.Split(new[] { before }, StringSplitOptions.None);
            return allAfter[1].Split(after)[0];
        }

        public static string ToLyd(this decimal number)
            => number == 0 ? string.Empty : number.ToString("##,##0.000");

        public static string ToLyd(this decimal number, DecimalPart decimalPart)
        {
            var value = number.ToString("##,##0.000");
            switch (decimalPart)
            {
                case DecimalPart.First:
                    return value.Split('.')[0];
                case DecimalPart.Second:
                    return value.Split('.')[1];
                default:
                    throw new ArgumentOutOfRangeException(nameof(decimalPart), decimalPart, null);
            }
        }

        public static string ToLydTwo(this decimal number) => number == 0 ? string.Empty : number.ToString("#0.000");

        public static string Ellipsis(this string s, int charsToDisplay = 30)
        {
            if (!string.IsNullOrWhiteSpace(s))
                return s.Length <= charsToDisplay ? s : new string(s.ToCharArray(0, charsToDisplay)) + "...";

            return string.Empty;
        }

        public static decimal ToDecimalField(this decimal number)
        {
            return number;
            //var decimalNumber = number.ToString("00.000");
            //var intNumber = number.ToString("####");

            //var third = decimalNumber.Substring(decimalNumber.Length - 1, 1);
            //var second = decimalNumber.Substring(decimalNumber.Length - 2, 1);
            //var first = decimalNumber.Substring(decimalNumber.Length - 3, 1);

            //if (third == "0")
            //{
            //    third = "";
            //}

            //if (second == "0" && third == "")
            //{
            //    second = "";
            //}

            //if (first == "0" && second == "")
            //{
            //    first = "";
            //}

            //var afterZero = first + second + third;

            //afterZero = afterZero == string.Empty ? "" : "." + afterZero;

            //var last = intNumber + afterZero;

            //return Convert.ToDecimal(last);
        }

        public static decimal ToRoundedDecimal(this decimal number)
        {
            return number;
            //var decimalNumber = number.ToString("##.000");
            //var intNumber = decimalNumber.Split('.')[0];

            //var third = decimalNumber.Substring(decimalNumber.Length - 1, 1);
            //var second = decimalNumber.Substring(decimalNumber.Length - 2, 1);
            //var first = decimalNumber.Substring(decimalNumber.Length - 3, 1);

            //if (int.Parse(first + second + third) >= 995)
            //{
            //    first = second = third = "";
            //    intNumber = (int.Parse(intNumber) + 1).ToString();
            //}

            //if (third == "0")
            //    third = "";

            //if (second == "0" && third == "")
            //    second = "";

            //if (first == "0" && second == "")
            //    first = "";

            //var afterZero = first + second + third;

            //afterZero = afterZero == string.Empty ? "" : "." + afterZero;

            //var last = intNumber + afterZero;

            //return Convert.ToDecimal(last);
        }
    }

    public enum DecimalPart
    {
        First,
        Second
    }
}