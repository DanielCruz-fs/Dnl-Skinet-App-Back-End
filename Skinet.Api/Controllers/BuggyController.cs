using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Errors;
using Skinet.Infrastructure.Data;

namespace Skinet.Api.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext store;

        public BuggyController(StoreContext store)
        {
            this.store = store;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = this.store.Products.Find(44);
            // For test global error hadling format response
            //throw new ArgumentException("xd error")

            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(thing);
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var thing = this.store.Products.Find(44);
            var thingToReturn = thing.ToString();
            return Ok(thingToReturn);
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest2(int id)
        {
            return Ok();
        }
    }
}
