using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class PasswordValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private bool _hasNumber;

        private bool _hasUperCase;

        private bool _hasSpecialChar;

        private PasswordValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder, bool hasNumber, bool hasUperCase, bool hasSpecialChar)
        {
            return new PasswordValidator<T>
            {
                _roleBuilder = roleBuilder,
                _hasNumber = hasNumber,
                _hasUperCase = hasUperCase,
                _hasSpecialChar = hasSpecialChar
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            if (fieldValue != null)
            {
                if (fieldValue.GetType() == typeof(string))
                {
                    var caracteres = ((string)fieldValue)?.ToArray();
                    var regex = new Regex("^[a-zA-Z0-9 ]*$");
                    var isNumber = true;
                    var isUpperCase = true;
                    var isSpecial = false;

                    foreach (var item in caracteres ?? new char[] { })
                    {
                        if (_hasNumber && isNumber)
                        {
                            isNumber = char.IsDigit(item);
                        }

                        if (_hasUperCase)
                        {
                            isUpperCase = char.IsUpper(item);
                        }

                        if (_hasSpecialChar)
                        {
                            isSpecial = regex.IsMatch(item.ToString());
                        }
                    }

                    if (!isNumber)
                    {
                        _roleBuilder.AddErrorMessage(fieldName, ValidationMessage.PasswordNumericChar(fieldName));
                    }

                    if (!isUpperCase)
                    {
                        _roleBuilder.AddErrorMessage(fieldName, ValidationMessage.PasswordNumericChar(fieldName));
                    }

                    if (!isSpecial)
                    {
                        _roleBuilder.AddErrorMessage(fieldName, ValidationMessage.PasswordNumericChar(fieldName));
                    }
                }
            }
        }
    }
}
