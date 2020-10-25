using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Dtos;
using Skinet.Api.Errors;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;

namespace Skinet.Api.Controllers
{
    public class ProductsController : BaseApiController 
    {
        private readonly IProductRepository productRepository;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(
            IProductRepository productRepository,
            // Generic repository implementation
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            // Automapper
            IMapper mapper
        )

        {
            this.productRepository = productRepository;
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            //var products = await this.productRepository.GetProductsAsync();

            // With specification pattern
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await this.productRepo.ListAsync(spec);

            // Without automapper
            //return Ok(products.Select(p => new ProductToReturnDto() { 
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    PictureUrl = p.PictureUrl,
            //    Price = p.Price,
            //    ProductBrand = p.ProductBrand.Name,
            //    ProductType = p.ProductType.Name
            //}).ToList());

            return Ok(this.mapper.Map<IList<Product>, IList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            //return Ok(await this.productRepository.GetProductByIdAsync(id));

            // With specification pattern
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await this.productRepo.GetEntityWithSpec(spec);

            // Without automapper
            //return Ok( new ProductToReturnDto() { 
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name
            //});

            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(this.mapper.Map<Product, ProductToReturnDto>(product));
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
