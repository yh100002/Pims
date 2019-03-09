
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;


namespace Utils.Upload
{

    public class Uploader : IUploader
    {
        public bool Upload(IFormFile file, string path, out string savePath)
        {                         
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                savePath = fullPath;

                return true;
            }
            else
            {
                savePath = null;
                return false;
            }
        }            
    }
}