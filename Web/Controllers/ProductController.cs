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
using Http;

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

        public ProductController(IOptions<PimsSettings> pimsSettings, IUploader uploader, ICsvParse csvParse, IHttpClient apiClient)
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
                        var response = await this.apiClient.PostAsync(this.pimsSettings.Value.ProductCommandApiUrl + "/api/productcommand/create", row);
                    }                  
                    
                    //FileInfo savedFile = new FileInfo(savePath); 
                    //savedFile.Delete();

                    return Ok("Uploaded");
                }

                return BadRequest();               

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        /*
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
