using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this is the list of all products";
        }

        [HttpGet("{id}")]
        public object GetProductId(int id)
        {
            return new { Id = id, ProductTitle = "Pepper" };
        }
    }
}
