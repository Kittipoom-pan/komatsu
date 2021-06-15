using komatsu.api.Interface;
using komatsu.api.Model;
using komatsu.api.Model.Request;
using komatsu.api.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace komatsu.api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[action]")]
    public class AuthenController : Controller
    {
        private readonly IOptions<JwtConfig> _jwtConfig;
        private readonly IAuthenticationRepo _authenticaition;
        private readonly IEmailServiceRepo _emailService;
        public AuthenController(IOptions<JwtConfig> jwtConfig, IAuthenticationRepo authenticaition, IEmailServiceRepo emailService)
        {
            _authenticaition = authenticaition;
            _jwtConfig = jwtConfig;
            _emailService = emailService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("login")]
        public async Task<IActionResult> RegisterCampaignApplicationNew([FromBody] UserRequest user, string app_version, string device_type)
        {
            try
            {
                var result = await _authenticaition.Login(user.Email, user.Password);

                if (!result.Success) return UnprocessableEntity(result); 

                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest user, string app_version, string device_type)
        {
            try
            {
                var response = await _authenticaition.Register(user);

                if (!response.Status) return UnprocessableEntity(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("forgetpassword")]
        public async Task<IActionResult> ForgetPassword(UserRequest model, string app_version, string device_type)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email)) return UnprocessableEntity(new Response("Email not found."));

                var response = await _authenticaition.ForgetPassword(model.Email);

                if (!response.Status) return UnprocessableEntity(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [MapToApiVersion("1.0")]
        [ActionName("resetpassword")]
        public async Task<IActionResult> ResetPassword(RegisterRequest resetPasswordModel, string app_version, string device_type)
        {
            var response = await _authenticaition.ResetPassword(resetPasswordModel);

            if (!response.Status) return UnprocessableEntity(response);

            return Ok(response);
        }

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[ActionName("sendemail")]
        //public async Task<IActionResult> SendEmail([FromForm] Message message)
        //{
        //    try
        //    {
        //        //if (message.To == "") return Ok("Email not found");

        //        await _emailSender.SendEmailAsync(message);
        //        return Ok();

        //        //var messages = new Message(new string[] { "kittipoom@feyverly.com" }, "Test mail with Attachments", "This is the content from our mail with attachments.");

        //        //await _emailSender.SendEmailAsync(messages);

        //        return Ok(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = ex });
        //    }
        //}

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("sendemail")]
        public async Task<IActionResult> SendEmail(EmailData emailData)
        {
            try
            {
                var result = _emailService.SendEmail(emailData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest model, string app_version, string device_type)
        {
            string token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();

            var response = await _authenticaition.ChangePassword(model, token);

            if (!response.Status) return UnprocessableEntity(response);

            return Ok(response);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ActionName("logout")]
        public async Task<IActionResult> Logout(string app_version, string device_type)
        {
            string token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();

            var response = await _authenticaition.Logout(token);

            if (!response.Status) return UnprocessableEntity(response);

            return Ok(response);
        }
    }
}
