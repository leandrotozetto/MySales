using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Core.Notifications.Interfaces;

namespace MySales.Product.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Represents the values will be validated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Param<T>
    {
        /// <summary>
        /// Value that will be validated.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Argument's name that will be validated.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Notifications
        /// </summary>
        private readonly INotification _notification;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="value">Value that will be validated.</param>
        /// <param name="name">Argument's name that will be validated.</param>
        /// <param name="customException">Custom exception.</param>
        public Param(T value, string name, INotification notification)
        {
            Value = value;
            Name = name;
            _notification = notification;
        }

        public void AddNotification(string property, string message, NotificationType notificationType)
        {
            _notification.AddNotification(property, message, notificationType);
        }
    }
}
