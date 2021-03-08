using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Identifiers;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using System;
using System.Collections.Generic;

namespace MySales.Product.Api.Domain.Mappers
{
    public static class ProductMapper
    {
        //public static Aggregates.Interfaces.IProduct Map(ProductCommandDto productCommandDto)
        //{
        //    return Aggregates.Product.New(productCommandDto.Name, StatusEnum.New(productCommandDto.Status));
        //}

        public static InsertProductCommandRequest Map(ProductCommandDto productCommandDto, Guid tenantId)
        {
            return InsertProductCommandRequest.New(productCommandDto.Name, productCommandDto.Status, tenantId);
        }

        public static UpdateProductCommandResquest Map(ProductId productId, ProductCommandDto productDto, TenantId tenantId)
        {
            return UpdateProductCommandResquest.New(productDto.Name, productDto.Status, productId.Value, tenantId.Value);
        }

        public static ProductQueryDto Map(Aggregates.Interfaces.IProduct product)
        {
            if (product.IsEmpty)
            {
                return ProductQueryDto.Empty;
            }

            return ProductQueryDto.New(product.ProductId.Value, product.Name, product.Status.Value);
        }

        public static Aggregates.Interfaces.IProduct Map(IProductCommandRequest productCommandRequest)
        {
            //TODO: resolver
            //if (insertProductCommandRequest is null)
            //{
            //    return Aggregates.Interfaces.IProduct;
            //}

            return Aggregates.Product.New(
                productCommandRequest.Name, 
                StatusEnum.New(productCommandRequest.Status),
                TenantId.New(productCommandRequest.TenantId));
        }

        public static IEnumerable<ProductQueryDto> Map(IEnumerable<Aggregates.Interfaces.IProduct> products)
        {
            foreach (var product in products)
            {
                yield return Map(product);
            }
        }
    }
}
