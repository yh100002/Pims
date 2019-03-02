using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using Events;
using MassTransit;
using Models;

namespace Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductQueryController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public ProductQueryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

         [HttpGet("productlist")]
        public async Task<IActionResult> ProductList()
        {
            var repo =this.uow.GetRepositoryAsync<ProductData>();
            var result = await repo.GetListAsync();    
            return Ok(result);
        }

        /*

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
