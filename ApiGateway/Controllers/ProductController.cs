using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Versioning;
using AutoMapper;
using Newtonsoft.Json;

using ApiGateway.Configuration;
using Utils.Upload;
using Utils.Csv;
using Models;
using Utils.Http;
using ApiGateway.Dto;
using Persistence;
using Persistence.Paging;

namespace ApiGateway.Controllers
{   

    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IOptions<PimsSettings> pimsSettings;    
        private readonly IHttpClient apiClient;

        public ProductController(IOptions<PimsSettings> pimsSettings, IHttpClient apiClient)
        {
            this.pimsSettings = pimsSettings;               
            this.apiClient = apiClient;
        }       

        
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(int page, int size)
        {
            var response = await this.apiClient.GetStringAsync(this.pimsSettings.Value.ProductQueryApiUrl + $"/api/productreader/productlist?page={page}&size={size}");
            return Ok(response);
        }

        [HttpGet("getProduct/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var response = await this.apiClient.GetStringAsync(this.pimsSettings.Value.ProductQueryApiUrl + $"/api/productreader/getproduct/{id.ToUpper()}");
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {   
            var response = await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productwriter/delete", id);
            return Ok(response);
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductData product)
        {            
            //Create            
            var response = await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productwriter/create", product);
            return Ok(response);
        }

        [HttpPut("update")]
        public IActionResult Put([FromBody] ProductData product)
        {            
            //Update
            var response = this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productwriter/update", product);
            return Ok(response);
        }
    }
}
