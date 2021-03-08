using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.ValuesObjects;
using MySales.Product.Api.Domain.Identifiers;
using System;

namespace MySales.Product.Api.Domain.Aggregates.Interfaces
{
    public interface ISku: IEmpty<ISku>
    {
        DateTime CreationDate { get; }

        string Description { get; }

        string Name { get; }

        string PartNumber { get; }

        SizeId SizeId { get; }

        SkuId SkuId { get; }

        Status Status { get; }

        int? Stock { get; }

        TenantId TenantId { get; }

        ProductId ProductId { get; }

        DateTime? UpdateDate { get; }

        void ChangeSize(SizeId sizeid);
    }
}