using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (thing == null)
            {
                return NotFound();
            }

            return Ok();
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
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest2(int id)
        {
            return Ok();
        }
    }
}
