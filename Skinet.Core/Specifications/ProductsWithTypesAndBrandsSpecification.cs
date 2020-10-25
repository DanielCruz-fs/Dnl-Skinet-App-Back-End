using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort)
        {
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
            this.AddOrderBy(x => x.Name);

            switch (sort)
            {
                case "priceAsc":
                    this.AddOrderBy(x => x.Price);
                    break;

                case "priceDesc":
                    this.AddOrderByDescending(x => x.Price);
                    break;

                default:
                    this.AddOrderBy(x => x.Name);
                    break;
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
        }
    }
}