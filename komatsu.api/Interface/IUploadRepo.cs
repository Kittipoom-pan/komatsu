using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace komatsu.api.Interface
{
    public interface IUploadRepo
    {
        Task<string> Upload(List<IFormFile> Files);

    }
}
