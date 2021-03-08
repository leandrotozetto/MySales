using System.Collections.Generic;

namespace MySales.Product.Api.Domain.Core.Exceptions
{
    /// <summary>
    /// Represents a errors for entity of domain.
    /// </summary>
    public class DomainError
    {
        /// <summary>
        /// Property name.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Error's messages.
        /// </summary>
        public ICollection<string> Messages { get; set; }

        private DomainError() { }

        public static DomainError New(string propertyName, IEnumerable<string> messages)
        {
            var domainError = new DomainError()
            {
                Property = propertyName
            };

            domainError.AddMessages(messages);

            return domainError;
        }

        public static DomainError New(string propertyName, string message)
        {
            var domainError = new DomainError()
            {
                Property = propertyName
            };

            domainError.AddMessage(message);

            return domainError;
        }

        /// <summary>
        /// Add error messages.
        /// </summary>
        /// <param name="messages"></param>
        private void AddMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }

        /// <summary>
        /// Add error message.
        /// </summary>
        /// <param name="message"></param>
        private void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}