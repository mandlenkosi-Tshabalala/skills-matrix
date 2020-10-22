using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadAsync(IFileListEntry file);
    }
}
