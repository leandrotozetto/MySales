using MySales.Product.Api.Domain.Core.Exceptions;
using System.Linq;

namespace MySales.Product.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Result of validation
    /// </summary>
    public class ArgumentValidationResult
    {
        /// <summary>
        /// Validation's status.
        /// </summary>
        public bool IsValid => !Error?.Messages?.Any() ?? true;

        public readonly static ArgumentValidationResult Success;

        /// <summary>
        /// Error.
        /// </summary>
        public DomainError Error { get; private set; }

        private ArgumentValidationResult() { }

        static ArgumentValidationResult()
        {
            if(Success is null)
            {
                Success = new ArgumentValidationResult() { };
            }
        }

        public static ArgumentValidationResult New(DomainError domainError)
        {
            var result = new ArgumentValidationResult()
            {
                Error = domainError
            };

            return result;
        }
    }
}
