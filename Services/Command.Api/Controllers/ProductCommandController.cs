﻿using System;
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
            Console.WriteLine(product.Name);
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
/*
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
    }
    */
    }
}