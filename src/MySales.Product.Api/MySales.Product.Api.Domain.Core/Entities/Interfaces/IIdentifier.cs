using System;

namespace MySales.Product.Api.Domain.Core.Entities.Interfaces
{
    public interface IIdentifier
    {
        Guid Value { get; }

        bool IsEmpty { get; }
    }
}
