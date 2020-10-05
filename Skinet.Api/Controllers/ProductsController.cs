using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Interfaces;

namespace Skinet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await this.productRepository.GetProductsAsync();
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
            return Ok(await this.productRepository.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            return Ok(await this.productRepository.GetProductTypesAsync());
        }
    }
}
