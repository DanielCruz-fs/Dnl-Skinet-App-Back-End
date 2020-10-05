using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;

namespace Skinet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;

        public ProductsController(
            IProductRepository productRepository,
            // Generic repository implementation
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo
        )

        {
            this.productRepository = productRepository;
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            //var products = await this.productRepository.GetProductsAsync();

            // With specification pattern
            var spec = new ProductsWithTypesAndBrands();
            var products = await this.productRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductId(int id)
        {
            return Ok(await this.productRepository.GetProductByIdAsync(id));
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            //return Ok(await this.productRepository.GetProductBrandsAsync());
            return Ok(await this.productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            //return Ok(await this.productRepository.GetProductTypesAsync());
            return Ok(await this.productTypeRepo.ListAllAsync());
        }
    }
}
