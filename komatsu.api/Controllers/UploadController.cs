using komatsu.api.Interface;
using komatsu.api.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace komatsu.api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[action]")]
    public class UploadController : Controller
    {
        private readonly IUploadRepo _upload;
        public UploadController(IUploadRepo upload)
        {
            _upload = upload;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("upload")]
        //[Route("{reference_id}")]
        public async Task<IActionResult> UploadImage(List<IFormFile> Files, string app_version, string device_type)
        {
            try
            {
                var result = await _upload.Upload(Files);

                if (result == null) return UnprocessableEntity(new Response("Image not found"));

                return Ok(new Response(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
