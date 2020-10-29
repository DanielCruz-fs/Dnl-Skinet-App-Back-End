using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
            // Super advanced using of logical operators
            : base
            (
                  // Same as: x.ProductBrand.Id == brandId && x.ProductType.Id == typeId
                  x => (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
                       (!productSpecParams.BrandId.HasValue || x.ProductBrand.Id == productSpecParams.BrandId) &&
                       (!productSpecParams.TypeId.HasValue || x.ProductType.Id == productSpecParams.TypeId)
            )
        {
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
            this.AddOrderBy(x => x.Name);
            // -1 'cause at page number one we do not want to skip anything
            this.ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
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
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
        }
    }
}