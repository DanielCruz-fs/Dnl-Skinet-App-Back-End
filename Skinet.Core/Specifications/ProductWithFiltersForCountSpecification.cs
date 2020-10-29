using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
            // Super advanced using of logical operators
            : base(
                  // Same as: x.ProductBrand.Id == brandId && x.ProductType.Id == typeId
                  x => (!productSpecParams.BrandId.HasValue || x.ProductBrand.Id == productSpecParams.BrandId) &&
                       (!productSpecParams.TypeId.HasValue || x.ProductType.Id == productSpecParams.TypeId)
            )
        {

        }
    }
}
