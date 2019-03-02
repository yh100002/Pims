using System;
using Microsoft.AspNetCore.Http;

namespace WebCommon.Upload
{
    public interface IUploader
    {
         bool Upload(IFormFile file, string path, out string savePath);
    }
}
