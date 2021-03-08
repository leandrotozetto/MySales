using MySales.Product.Api.Domain.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Core.Validations.Interfaces
{
    public interface IRuleBuilder<T> where T : class
    {
        Collection<IPropertyValidation<T>> PropertiesValidations { get; }

        void AddRule(IRule rule);

        void AddErrorMessage(string name, string message);
        /// <summary>
        /// List with erros messages.
        /// </summary>
        IEnumerable<DomainError> Errors { get; }

        string CustomNameField { get; }

        void When(Expression<Func<T, bool>> condition);

        bool IsValidRule(T entity);
    }
}
