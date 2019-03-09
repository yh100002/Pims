using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Events;
using MassTransit;
using Models;

namespace ProductReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReaderController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public ProductReaderController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet("productlist")]
        public async Task<IActionResult> ProductList(int page, int size)
        {
            var repo =this.uow.GetRepositoryAsync<ProductData>();
            var result = await repo.GetListAsync(index:page, size:size, orderBy:q => q.OrderByDescending(o => o.Timestamp));    
            //var result = await repo.GetListAsync(index:page, size:size);    
            return Ok(result);
        }
         [HttpGet("getProduct/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var repo =this.uow.GetRepositoryAsync<ProductData>();
            var result = await repo.SingleAsync(s => s.ZamroID == id);
            return Ok(result);
        }
        
    }
}
