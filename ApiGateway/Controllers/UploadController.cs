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
    public class UploadController : ControllerBase
    {
        private readonly IOptions<PimsSettings> pimsSettings;    
        private readonly IUploader uploader;
        private readonly ICsvParse csvParse;        
        private readonly IHttpClient apiClient;

        public UploadController(IOptions<PimsSettings> pimsSettings, IUploader uploader, ICsvParse csvParse, IHttpClient apiClient)
        {
            this.pimsSettings = pimsSettings;    
            this.uploader = uploader;  
            this.csvParse = csvParse; 
            this.apiClient = apiClient;                
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
                        var response = await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productwriter/create", row);
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

    }
}