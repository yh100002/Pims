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
        
    }
}
