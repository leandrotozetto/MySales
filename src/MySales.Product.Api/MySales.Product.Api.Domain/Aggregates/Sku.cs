using MySales.Product.Api.Domain.Aggregates.Interfaces;
using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.ValuesObjects;
using MySales.Product.Api.Domain.Identifiers;
using System;

namespace MySales.Product.Api.Domain.Aggregates
{
    public class Sku : IEntity, ISku
    {
        public SkuId SkuId { get; private set; }

        public string PartNumber { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int? Stock { get; private set; }

        public SizeId SizeId { get; private set; }

        public Status Status { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public TenantId TenantId { get; private set; }

        public ProductId ProductId { get; private set; }

        private static readonly ISku _empty;

        public ISku Empty => _empty;

        public bool IsEmpty => Equals(_empty);


        private Sku() { }

        static Sku()
        {
            _empty ??= new Sku();
        }

        public static ISku New(string partNumber, string description, int stock, SizeId sizeId, Status status, TenantId tenantId)
        {
            return new Sku()
            {
                Description = description,
                PartNumber = partNumber,
                SizeId = sizeId,
                Stock = stock,
                SkuId = SkuId.New(),
                CreationDate = DateTime.Now,
                Status = status,
                TenantId = tenantId
            };
        }

        public static ISku New(SkuId skuId, string partNumber, string description, int stock, SizeId sizeId, Status status, TenantId tenantId, DateTime creationDate)
        {
            return new Sku()
            {
                Description = description,
                PartNumber = partNumber,
                SizeId = sizeId,
                Stock = stock,
                SkuId = skuId,
                CreationDate = creationDate,
                UpdateDate = DateTime.Now,
                Status = status,
                TenantId = tenantId
            };
        }

        public void ChangeSize(SizeId sizeid)
        {
            SizeId = sizeid;
            UpdateDate = DateTime.Now;
        }
    }
}
