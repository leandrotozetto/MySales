using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.Enum;
using System;

namespace MySales.Product.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Extensions that contains the methods to valid the arguments.
    /// </summary>
    public static class ParamExtensions
    {
        /// <summary>
        /// Checks if entity is not null.
        /// </summary>
        /// <typeparam name="T">argument's type.</typeparam>
        /// <param name="param">Data that will be validated.</param>
        public static void EntityIsNotNull<T>(this Param<T> param)
        {
            if (param.Value == null)
            {
                param.AddNotification(param.Name, DomainMessage.EntityIsNull(param.Name), NotificationTypeEnum.Information);
            }
        }

        /// <summary>
        /// Checks if arg is not null.
        /// </summary>
        /// <typeparam name="T">argument's type.</typeparam>
        /// <param name="param">Data that will be validated.</param>
        public static void ArgumentIsNotNull<T>(this Param<T> param)
        {
            if (param.Value == null)
            {
                param.AddNotification(param.Name, DomainMessage.ArgumentIsNull(param.Name), NotificationTypeEnum.Information);
            }
        }

        /// <summary>
        /// Checks if arg is not null.
        /// </summary>
        /// <param name="param">Data that will be validated.</param>
        public static void IsNullOrWhiteSpace(this Param<string> param)
        {
            if (string.IsNullOrWhiteSpace(param.Value))
            {
                param.AddNotification(param.Name, DomainMessage.IsNullOrWhiteSpace(nameof(param.Name)), NotificationTypeEnum.Information);
            }
        }

        /// <summary>
        /// Checks is the entity is not null.
        /// </summary>
        /// <typeparam name="T">argument's type.</typeparam>
        /// <param name="param">Data that will be validated.</param>
        public static void EntityExists<T>(this Param<T> param)
        {
            if (param.Value == null)
            {
                param.AddNotification(param.Name, DomainMessage.IdNotExist(param.Name), NotificationTypeEnum.Information);
            }
        }

        /// <summary>
        /// Checks if the Guid is not empty
        /// </summary>
        /// <param name="param">Data that will be validated.</param>
        public static IEnsureResult IdIsNotEmpty(this Param<IIdentifier> param)
        {
            if (param.Value.IsEmpty)
            {
                param.AddNotification(param.Name, DomainMessage.IdNotExist(param.Name), NotificationTypeEnum.Information);

                return EnsureResultError.New();
            }

            return EnsureResultSuccess.New();
        }

        /// <summary>
        /// Checks if the int is greather than zero
        /// </summary>
        /// <param name="param">Data that will be validated.</param>
        public static void HasValue(this Param<int> param)
        {
            if (param.Value == 0)
            {
                param.AddNotification(param.Name, DomainMessage.IdNotExist(param.Name), NotificationTypeEnum.Information);
            }
        }
    }
}
