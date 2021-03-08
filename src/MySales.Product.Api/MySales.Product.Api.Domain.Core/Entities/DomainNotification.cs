using MySales.Product.Api.Domain.Core.Enum;
using System.Collections.Generic;

namespace MySales.Product.Api.Domain.Core.Entities
{
    public class DomainNotification : IDomainNotification
    {
        public string Key { get; private set; }

        public IReadOnlyList<string> Messages => _messages.AsReadOnly();

        public NotificationType NotificationType { get; private set; }

        private readonly List<string> _messages = new List<string>();

        public static DomainNotification New(string key, IEnumerable<string> messages, NotificationType notificationType)
        {
            var domainNotification = new DomainNotification
            {
                Key = key,
                NotificationType = notificationType
            };

            domainNotification._messages.AddRange(messages);

            return domainNotification;
        }

        public static DomainNotification New(string key, string message, NotificationType notificationType)
        {
            var domainNotification = new DomainNotification
            {
                Key = key,
                NotificationType = notificationType
            };

            domainNotification._messages.Add(message);

            return domainNotification;
        }

        public void AddMessage(IEnumerable<string> messages)
        {
            _messages.AddRange(messages);
        }

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }

    public interface IDomainNotification
    {
        public string Key { get; }

        public IReadOnlyList<string> Messages { get; }
    }
}
