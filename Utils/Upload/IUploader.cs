using System;
using Microsoft.AspNetCore.Http;

namespace Utils.Upload
{
    public interface IUploader
    {
         bool Upload(IFormFile file, string path, out string savePath);
    }
}
