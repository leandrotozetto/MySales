using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Enum;
using System.Collections.Generic;

namespace MySales.Product.Api.Domain.Core.Notifications.Interfaces
{
    public interface INotification
    {
        void AddNotification(string key, string value, NotificationType notificationType);

        bool HasErrors { get; }

        int ErrorCode { get; }

        IEnumerable<IDomainNotification> Errors { get; }
    }
}
