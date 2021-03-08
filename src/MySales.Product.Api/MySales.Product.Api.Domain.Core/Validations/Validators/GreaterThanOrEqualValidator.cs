using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;

namespace MySales.Product.Api.Domain.Core.Validations.Validators
{
    public class GreaterThanOrEqualValidator<T> : IRule where T : class
    {
        private IRuleBuilder<T> _roleBuilder;

        private dynamic _valueToComare;

        private GreaterThanOrEqualValidator() { }

        public static IRule New(IRuleBuilder<T> roleBuilder, dynamic valueToComare)
        {
            return new GreaterThanOrEqualValidator<T>
            {
                _roleBuilder = roleBuilder,
                _valueToComare = valueToComare
            };
        }

        public void Validate(string fieldName, dynamic fieldValue)
        {
            if (fieldValue != null)
            {
                switch (fieldValue.GetType())
                {
                    case Type a when (a == typeof(int) || a == typeof(int?) && a.GetType() == _valueToComare.GetType()):
                    case Type b when (b == typeof(uint) || b == typeof(uint?) && b.GetType() == _valueToComare.GetType()):
                    case Type c when (c == typeof(long) || c == typeof(long?) && c.GetType() == _valueToComare.GetType()):
                    case Type d when (d == typeof(ulong) || d == typeof(ulong?) && d.GetType() == _valueToComare.GetType()):
                    case Type e when (e == typeof(short) || e == typeof(short?) && e.GetType() == _valueToComare.GetType()):
                    case Type f when (f == typeof(ushort) || f == typeof(ushort?) && f.GetType() == _valueToComare.GetType()):
                    case Type g when (g == typeof(double) || g == typeof(double?) && g.GetType() == _valueToComare.GetType()):
                    case Type h when (h == typeof(decimal) || h == typeof(decimal?) && h.GetType() == _valueToComare.GetType()):
                    case Type i when (i == typeof(byte) || i == typeof(byte?) && i.GetType() == _valueToComare.GetType()):
                    case Type j when (j == typeof(sbyte) || j == typeof(sbyte?) && j.GetType() == _valueToComare.GetType()):
                        if ((fieldValue == null && fieldValue != _valueToComare) || fieldValue < _valueToComare)
                        {
                            _roleBuilder.AddErrorMessage(fieldName, fieldValue);
                        }
                        break;
                    default:
                        throw new Exception("The property's type isn't allowed in method GreaterThanOrEqual of class Validation.");
                }
            }
        }
    }
}
