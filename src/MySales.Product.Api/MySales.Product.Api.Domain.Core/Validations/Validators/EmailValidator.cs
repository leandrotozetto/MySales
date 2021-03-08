using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System.Net.Mail;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class EmailValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private EmailValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder)
        {
            return new EmailValidator<T>
            {
                _roleBuilder = roleBuilder
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            if (fieldValue != null)
            {
                var message = ValidationMessage.Email(fieldName);

                if (fieldValue.GetType() == typeof(string))
                {
                    try
                    {
                        MailAddress m = new MailAddress(fieldValue);
                    }
                    catch
                    {
                        _roleBuilder.AddErrorMessage(fieldName, message);
                    }
                }
            }
        }
    }
}