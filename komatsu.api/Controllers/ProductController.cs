using komatsu.api.DBContexts;
using komatsu.api.Interface;
using komatsu.api.Model.Request;
using komatsu.api.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace komatsu.api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[action]")]
    public class ProductController : Controller
    {
        private readonly devkomatsuContext _context;
        private readonly IResultRepo _resultRepo;
        public ProductController(devkomatsuContext context, IResultRepo resultRepo)
        {
            _context = context;
            _resultRepo = resultRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ActionName("model")]
        public async Task<IActionResult> GetModel(string app_version, string device_type)
        {
            try
            {
                return Ok(new { data = _context.MasterModel.ToList() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("calculate-tco")]
        public async Task<IActionResult> GetCalculateTCO(CalculateTCORequest calculateTCO, string app_version, string device_type)
        {
            try
            {
                string token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();

                var result = await _resultRepo.GetCalculateTCO(calculateTCO, token);

                if (result == null) return UnprocessableEntity(new Response("Data not found"));

                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("calculate-3yc")]
        //[Route("economy")]
        public async Task<IActionResult> GetCalculateTYC(Calculate3YCRequest calculate3YC, string app_version, string device_type)
        {
            try
            {
                string token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();

                var result = await _resultRepo.GetCalculate3YC(calculate3YC, token);

                if (result == null) return UnprocessableEntity(new Response("Data not found"));

                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ActionName("history")]
        public async Task<IActionResult> GetHistory(string app_version, string device_type, string type)
        {
            try
            {
                string token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();

                var result = await _resultRepo.GetHistory(token, type, app_version, device_type);

                if (result == null) return UnprocessableEntity(new Response("User not found"));

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ActionName("checksystem")]
        public async Task<IActionResult> CheckSystem(string app_version, string device_type)
        {
            try
            {
                var response = await _resultRepo.CheckSystem(app_version, device_type);

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
