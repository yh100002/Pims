using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Events;
using MassTransit;

namespace Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCommandController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IBus messageBus;
        public ProductCommandController(IUnitOfWork uow, IBus messageBus)
        {
            this.uow = uow;
            this.messageBus = messageBus;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductData product)
        {
            var repo = this.uow.GetRepositoryAsync<ProductData>();               
            var existed = repo.SingleAsync(s => s.ZamroID == product.ZamroID);
            if(existed.Result != null)
            {
                //Console.WriteLine("Updated:" + existed.Result.Name);
                repo.UpdateAsync(existed.Result);
                this.uow.SaveChanges();
                 await this.messageBus.Publish<ProductUpdateEvent>(new 
                    { product.ZamroID, 
                        product.Name, 
                        product.Description, 
                        product.MinOrderQuantity,
                        product.UnitOfMeasure, 
                        product.CategoryID, 
                        product.PurchasePrice, 
                        product.Available }
                    );
                return Ok();               
            }
            //Console.WriteLine("Created:" + product.Name);
            await repo.AddAsync(product);    
            this.uow.SaveChanges();   

            await this.messageBus.Publish<ProductCreateEvent>(new 
                { product.ZamroID, 
                    product.Name, 
                    product.Description, 
                    product.MinOrderQuantity,
                    product.UnitOfMeasure, 
                    product.CategoryID, 
                    product.PurchasePrice, 
                    product.Available }
                );

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] string zamroID)
        {            
            var repo = this.uow.GetRepository<ProductData>();
            repo.Delete(zamroID);   
            this.uow.SaveChanges();
            await this.messageBus.Publish<ProductDeleteEvent>(new { zamroID } );
            return Ok("delete");
        }
    }
}
