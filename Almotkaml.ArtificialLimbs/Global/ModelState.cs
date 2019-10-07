using Almotkaml.ArtificialLimbs.Global.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Web;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Global.Attributes;

namespace Almotkaml.ArtificialLimbs.Global
{
    public class ModelState
    {
        internal ModelState() { }
        public bool IsValid<TModel>(TModel model)
        {
            Clear();
            var validatableModel = model as IValidatable;
            //validatableModel?.Validate(this);

            var modelType = typeof(TModel);
            var context = new ValidationContext(model);

            foreach (var property in modelType.GetRuntimeProperties())
            {
                var attributes = property.GetCustomAttributes(false).OfType<ValidationAttribute>().ToList();
                var value = property.GetValue(model);
                property.PropertyType.GetRuntimeProperties();

                foreach (var attribute in attributes)
                {
                    try
                    {
                        if (attribute.IsValid(value))
                        {
                            Properties.Add(new PropertyState()
                            {
                                IsValid = true,
                                Name = property.Name
                            });
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        Properties.Add(new PropertyState()
                        {
                            IsValid = false,
                            ErrorMessage = attribute.ErrorMessage,
                            Name = property.Name
                        });
                        continue;
                    }

                    context.DisplayName = GetDisplayName(property);

                    var validationResult = attribute.GetValidationResult(value, context);

                    if (validationResult != null)
                    {
                        Properties.Add(new PropertyState()
                        {
                            IsValid = false,
                            ErrorMessage = ReformatValidationResult(validationResult.ErrorMessage, context.DisplayName),
                            Name = property.Name
                        });
                    }
                }


                PropertiesAreValid(property, property.Name, value);
            }

            // after complete , return the result of the additional validations ...
            return ModelIsValid;
        }

        public bool ModelIsValid => Properties.All(p => p.IsValid);
        public IList<PropertyState> Properties { get; } = new List<PropertyState>();
        public void Clear() => Properties.Clear();
        public string ValidationMessages => string.Join(" ", Properties.Select(p => p.ErrorMessage));

        public bool AddError(Expression<Func<object, object>> expression, string message)
            => AddError(expression.ToExpressionTarget(), message);
        public bool AddError(string propertyName, string message)
        {
            Properties.Add(new PropertyState()
            {
                Name = propertyName,
                ErrorMessage = message,
                IsValid = false
            });

            return false;
        }
        public bool AddError(string message)
        {
            Properties.Add(new PropertyState()
            {
                ErrorMessage = message,
                IsValid = false
            });

            return false;
        }


        private void PropertiesAreValid(PropertyInfo prop, string propName, object propValue)
        {
            if (propValue == null)
                return;

            // don't look in non system types ...
            if (prop.PropertyType.Namespace == "System")
                return;

            if (prop.PropertyType.Namespace.Contains("System.Collections"))
                return;

            var context = new ValidationContext(prop);
            foreach (var property in prop.PropertyType.GetRuntimeProperties())
            {
                var attributes = property.GetCustomAttributes(false).OfType<ValidationAttribute>().ToList();

                object value;
                try
                {
                    value = propValue == null
                        ? null
                        : property.GetValue(propValue);
                }
                catch
                {
                    value = null;
                }

                foreach (var attribute in attributes)
                {
                    try
                    {
                        if (attribute.IsValid(value))
                        {
                            Properties.Add(new PropertyState()
                            {
                                IsValid = true,
                                Name = property.Name
                            });
                            continue;
                        }
                    }
                    catch
                    {
                        Properties.Add(new PropertyState()
                        {
                            IsValid = false,
                            ErrorMessage = attribute.ErrorMessage,
                            Name = property.Name
                        });
                        continue;
                    }

                    context.DisplayName = GetDisplayName(property);
                    var validationResult = attribute.GetValidationResult(value, context);

                    if (validationResult != null)
                    {
                        Properties.Add(new PropertyState()
                        {
                            IsValid = false,
                            ErrorMessage = ReformatValidationResult(validationResult.ErrorMessage, context.DisplayName),
                            Name = propName + '.' + property.Name
                        });
                    }
                }


                PropertiesAreValid(property, propName + '.' + property.Name, value);
            }
        }

        private static string GetDisplayName(PropertyInfo info)
        {
            var title = info.GetCustomAttribute<ResourceBasedAttribute>()?.Display;

            if (title != null)
                return title;

            return info.GetCustomAttribute<DisplayAttribute>()?.Name ?? info.Name;
        }
        private static string ReformatValidationResult(string result, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(result))
                return result;

            if (result.Contains("{") && result.Contains("}"))
                return string.Format(result, parameterName);

            return result;
        }
    }
}