using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class DateValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private DateValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder)
        {
            return new DateValidator<T>
            {
                _roleBuilder = roleBuilder
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            if (fieldValue != null)
            {
                DateTime date = DateTime.MinValue;
                bool isInvalid = false;

                switch (fieldValue.GetType())
                {
                    case Type x when (x == typeof(string)):
                        if (string.IsNullOrWhiteSpace(fieldValue) || DateTime.TryParse(fieldValue, out date) || IsDefaultValue(date))
                        {
                            isInvalid = true;
                        }
                        break;
                    case Type a when (a == typeof(DateTime) || a == typeof(DateTime?)):
                        if (fieldValue == null || IsDefaultValue(fieldValue))
                        {
                            isInvalid = true;
                        }
                        break;
                    default:
                        throw new Exception("The property's type isn't allowed in method Date of class Validation.");
                }

                static bool IsDefaultValue(DateTime value)
                {
                    return value == DateTime.MinValue || value == DateTime.MaxValue;
                }

                if (isInvalid)
                {
                    _roleBuilder.AddErrorMessage(fieldName, ValidationMessage.Date(fieldName));
                }
            }
        }
    }
}
