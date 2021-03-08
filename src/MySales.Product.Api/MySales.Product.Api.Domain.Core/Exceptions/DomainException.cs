using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MySales.Product.Api.Domain.Core.Exceptions
{
    /// <summary>
    /// Exception with the errors of domain validations.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Errors message.
        /// </summary>
        public IEnumerable<DomainError> Errors { get; private set; }

        /// <summary>
        /// Creates a new instance of the DomainException.
        /// </summary>
        /// <param name="erros">List of errors message.</param>
        public DomainException(IEnumerable<DomainError> erros)
        {
            Errors = erros;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="Property">Property related with error.</param>
        /// <param name="message">Error message.</param>
        public DomainException(string Property, string message)
        {
            Errors = new Collection<DomainError>
            {
                 DomainError.New(Property, new List<string> { message })
            };
        }
    }
}