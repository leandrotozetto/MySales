using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Core.Notifications.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MySales.Product.Api.Domain.Core.Notifications
{
    public class Notification : INotification
    {
        /// <summary>
        /// Notification Type
        /// </summary>
        public int ErrorCode => _errors.Max(x => x.NotificationType.Value);

        private readonly ICollection<DomainNotification> _errors = new Collection<DomainNotification>();

        public bool HasErrors => _errors.Count > 0;

        public IEnumerable<IDomainNotification> Errors => _errors.Select(x=> x as IDomainNotification);

        public void AddNotification(string key, IEnumerable<string> messages, NotificationType notificationType)
        {
            var error = _errors.FirstOrDefault(x => x.Key == key);

            if (error != null)
            {
                error.AddMessage(messages);

                return;
            }

            var notification = DomainNotification.New(key, messages, notificationType);

            _errors.Add(notification);
        }

        public void AddNotification(string key, string message, NotificationType notificationType)
        {
            var error = _errors.FirstOrDefault(x => x.Key == key);

            if (error != null)
            {
                error.AddMessage(message);

                return;
            }

            var notification = DomainNotification.New(key, message, notificationType);

            _errors.Add(notification);
        }
    }
}
