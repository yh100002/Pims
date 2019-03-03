using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Configuration;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebCommon.Upload;
using WebCommon.Csv;
using Models;
using Http;
using Web.Dto;
using AutoMapper;
using Newtonsoft.Json;
using Data;
using Data.Paging;

namespace Web.Controllers
{   

    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IOptions<PimsSettings> pimsSettings;    
        private readonly IUploader uploader;
        private readonly ICsvParse csvParse;
        private readonly IHttpClient apiClient;
        private readonly IMapper mapper;


        public ProductController(IOptions<PimsSettings> pimsSettings, IUploader uploader, ICsvParse csvParse, IHttpClient apiClient, IMapper mapper)
        {
            this.pimsSettings = pimsSettings;    
            this.uploader = uploader;  
            this.csvParse = csvParse; 
            this.apiClient = apiClient;    
            this.mapper = mapper; 
        }


        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];    
                var result = this.uploader.Upload(file, this.pimsSettings.Value.UploadFolder, out string savePath);

                if(result)
                {
                    var csvResults = this.csvParse.Parse(savePath);
                    foreach(var row in csvResults)
                    {
                        //Create or Update
                        var response = await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productcommand/create", row);
                    }

                    return Ok("Uploaded");
                }

                return BadRequest();               

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await this.apiClient.GetStringAsync(this.pimsSettings.Value.ProductQueryApiUrl + "/api/productquery/productlist");
            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {           
            await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productcommand/delete", id);
        }

        [HttpPut("update")]
        public async Task Put([FromBody] ProductData product)
        {            
            //Create or Update
            await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productcommand/create", product);
        }
    }
}
