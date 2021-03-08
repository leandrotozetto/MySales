using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class NotNullValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private NotNullValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder)
        {
            return new NotNullValidator<T>
            {
                _roleBuilder = roleBuilder
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            var isInvalid = false;
            var message = ValidationMessage.NotNull(fieldName);

            if (fieldValue is null)
            {
                isInvalid = true;
            }            

            if (isInvalid)
            {
                _roleBuilder.AddErrorMessage(fieldName, message);
            }
        }
    }
}
