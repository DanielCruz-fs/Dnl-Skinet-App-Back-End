using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Skinet.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                // ProductBrands
                if (!storeContext.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Skinet.Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        storeContext.ProductBrands.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

                // ProductTypes
                if (!storeContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Skinet.Infrastructure/Data/SeedData/types.json");
                    var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        storeContext.ProductTypes.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

                if (!storeContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Skinet.Infrastructure/Data/SeedData/products.json");
                    var products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(productsData);

                    /* 
                     * ADDING DATA WITHOUT EXTRA CALLS TO DATABASE WORKS FINE USING "FAKES" OR "STUBS"
                     * (NOT WORKING WHEN READING FROM A JSON FILE PROBLEMS WITH ATTACH METHOD)
                     */

                    //var products = new List<Product>()
                    //{
                    //    new Product ()
                    //    {
                    //        Name = "test1",
                    //        Description = "test1",
                    //        PictureUrl = "test1",
                    //        Price = 34,
                    //        ProductBrand = new ProductBrand() { Id = 1 },
                    //        ProductType = new ProductType() { Id = 1 },
                    //    },
                    //    new Product ()
                    //    {
                    //        Name = "test2",
                    //        Description = "test2",
                    //        PictureUrl = "test2",
                    //        Price = 33,
                    //        ProductBrand = new ProductBrand() { Id = 2 },
                    //        ProductType = new ProductType() { Id = 2 },
                    //    },
                    //};

                    foreach (var item in products)
                    {
                        //var brand = new ProductBrand() { Id = item.ProductBrand.Id };
                        //storeContext.ProductBrands.Attach(brand);
                        //var type = new ProductType() { Id = item.ProductType.Id };
                        //storeContext.Attach(type);
                        //var product = new Product()
                        //{
                        //    Name = item.Name,
                        //    Description = item.Description,
                        //    PictureUrl = item.PictureUrl,
                        //    Price = item.Price,
                        //    ProductBrand = brand,
                        //    ProductType = type
                        //};
                        //storeContext.Products.Add(product);

                        var product = new Product()
                        {
                            Name = item.Name,
                            Description = item.Description,
                            PictureUrl = item.PictureUrl,
                            Price = item.Price,
                            ProductBrand = await storeContext.ProductBrands.FindAsync(item.ProductBrand.Id),
                            ProductType = await storeContext.ProductTypes.FindAsync(item.ProductType.Id)
                        };

                        storeContext.Products.Add(product);
                    }

                    await storeContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.InnerException.ToString());
            }
        }
    }
}
