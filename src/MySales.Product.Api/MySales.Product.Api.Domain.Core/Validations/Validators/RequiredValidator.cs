using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class RequiredValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private RequiredValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder)
        {
            return new RequiredValidator<T>
            {
                _roleBuilder = roleBuilder
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            var isInvalid = false;
            var message = ValidationMessage.Required(fieldName);

            if (fieldValue is null)
            {
                isInvalid = true;
            }
            else
            {
                switch (fieldValue.GetType())
                {
                    case Type x when x == typeof(string):
                        if (string.IsNullOrWhiteSpace(fieldValue))
                        {
                            isInvalid = true;
                        }
                        break;
                    case Type a when a == typeof(int?):
                    case Type b when b == typeof(uint?):
                    case Type c when c == typeof(long?):
                    case Type d when d == typeof(ulong?):
                    case Type e when e == typeof(short?):
                    case Type f when f == typeof(ushort?):
                    case Type g when g == typeof(double?):
                    case Type h when h == typeof(decimal?):
                    case Type i when i == typeof(byte?):
                    case Type j when j == typeof(sbyte?):
                    case Type k when k == typeof(char?):
                    case Type l when l == typeof(bool?):
                        if (fieldValue == null)
                        {
                            isInvalid = true;
                        }
                        break;
                    case Type x when (x == typeof(Guid) || x == typeof(Guid?)):
                        if (fieldValue == Guid.Empty || fieldValue == null)
                        {
                            isInvalid = true;
                        }
                        break;
                    case Type x when x == typeof(int):
                        isInvalid = (int)fieldValue == default;
                        break;
                    case Type x when x == typeof(uint):
                        isInvalid = (uint)fieldValue == default;
                        break;
                    case Type x when x == typeof(long):
                        isInvalid = (long)fieldValue == default;
                        break;
                    case Type x when x == typeof(ulong):
                        isInvalid = (ulong)fieldValue == default;
                        break;
                    case Type x when x == typeof(short):
                        isInvalid = (short)fieldValue == default;
                        break;
                    case Type x when x == typeof(ushort):
                        isInvalid = (ushort)fieldValue == default;
                        break;
                    case Type x when x == typeof(double):
                        isInvalid = (double)fieldValue == default;
                        break;
                    case Type x when x == typeof(decimal):
                        isInvalid = (decimal)fieldValue == default;
                        break;
                    case Type x when x == typeof(byte):
                        isInvalid = (byte)fieldValue == default;
                        break;
                    case Type x when x == typeof(sbyte):
                        isInvalid = (sbyte)fieldValue == default;
                        break;
                    case Type x when x == typeof(char):
                        isInvalid = (char)fieldValue == default;
                        break;
                    case Type x when x == typeof(bool):
                        isInvalid = (bool)fieldValue == default;
                        break;
                }
            }

            if (isInvalid)
            {
                _roleBuilder.AddErrorMessage(fieldName, message);
            }
        }
    }
}
