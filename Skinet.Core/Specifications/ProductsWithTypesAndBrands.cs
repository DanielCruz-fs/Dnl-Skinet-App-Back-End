using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class ProductsWithTypesAndBrands : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrands()
        {
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
        }
    }
}