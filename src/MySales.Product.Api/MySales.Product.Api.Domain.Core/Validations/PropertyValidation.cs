using MySales.Product.Api.Domain.Core.Validations.Argument;
using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Core.Validations
{
    public class PropertyValidation<T> : IPropertyValidation<T>
    {
        private Expression<Func<T, dynamic>> _expressionProperty;

        public ICollection<IRule> Rules { get; private set; }

        private PropertyValidation() { }

        public static IPropertyValidation<T> New(Expression<Func<T, dynamic>> expression)
        {
            return new PropertyValidation<T>
            {
                _expressionProperty = expression,
                Rules = new Collection<IRule>()
            };
        }

        public PropertyValues Compile(T entity)
        {
            //Ensure.That(entity, nameof(entity)).EntityIsNotNull<T>();

            //var memberExpression = (MemberExpression)expressionProperty.Body;
            var memberExpression = GetMemberExpression(_expressionProperty);
            var dynamicFunction = _expressionProperty.Compile();

            return new PropertyValues(memberExpression.Member.Name, dynamicFunction.Invoke(entity)); ;
        }

        /// <summary>
        /// Get the MemberExpression from Expression.
        /// </summary>
        /// <returns>Returns the MemberExpression <see cref="MemberExpression"/>.</returns>
        private MemberExpression GetMemberExpression(Expression<Func<T, dynamic>> expressionProperty)
        {
            switch (expressionProperty.Body)
            {
                case MemberExpression _:
                    return expressionProperty.Body as MemberExpression;
                default:
                    {
                        var op = ((UnaryExpression)expressionProperty.Body).Operand;
                        return op as MemberExpression;
                    }
            }
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void AddRule(IRule rule)
        {
            Rules.Add(rule);
        }
    }
}