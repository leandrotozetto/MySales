using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System.Text.RegularExpressions;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class NumericValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private NumericValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder)
        {
            return new NumericValidator<T>
            {
                _roleBuilder = roleBuilder
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            if (fieldValue != null)
            {
                var message = ValidationMessage.Numeric(fieldName);

                Regex regex = new Regex(@"^\d$");

                if (!regex.IsMatch(fieldValue))
                {
                    _roleBuilder.AddErrorMessage(fieldName, message);
                }
            }
        }
    }
}
