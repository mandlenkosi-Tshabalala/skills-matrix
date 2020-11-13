using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadAsync(IFileListEntry file)
        {
            var path = Path.Combine(_environment.ContentRootPath, "wwwroot//images", file.Name);
            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(fileStream);
            }

            return path;
        }
    }
}
