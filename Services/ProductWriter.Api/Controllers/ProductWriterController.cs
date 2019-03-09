using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Models;
using Events;
using MassTransit;

namespace ProductWriter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWriterController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IBus messageBus;
        public ProductWriterController(IUnitOfWork uow, IBus messageBus)
        {
            this.uow = uow;
            this.messageBus = messageBus;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductData product)
        {
            var repo = this.uow.GetRepositoryAsync<ProductData>();    
            //Console.WriteLine("Created:=========================>" + product.ZamroID);
            product.ZamroID = product.ZamroID ?? Guid.NewGuid().ToString();
            product.Timestamp = DateTime.Now;
            var existed = await repo.SingleAsync(s => s.ZamroID == product.ZamroID);
            if(existed == null)
            {
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
            }
            
            return Ok(product);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ProductData product)
        {
            var repo = this.uow.GetRepositoryAsync<ProductData>();               
            Console.WriteLine("Updated==========================>" + product.ZamroID);
            product.Timestamp = DateTime.Now;
            repo.UpdateAsync(product);
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
            return Ok(product);               
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
