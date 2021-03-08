using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Core.ValuesObjects;
using System;

namespace MySales.Product.Api.Domain.Core.Entities.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Status of register.
        /// </summary>
        Status Status { get; }

        /// <summary>
        /// Date of create
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// Date of Update
        /// </summary>
        DateTime? UpdateDate { get; }

        /// <summary>
        /// Cliente Id
        /// </summary>
        TenantId TenantId { get; }
    }
}
