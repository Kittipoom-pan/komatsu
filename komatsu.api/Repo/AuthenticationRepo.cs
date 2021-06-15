using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using komatsu.api.DBContexts;
using komatsu.api.Interface;
using komatsu.api.Model;
using komatsu.api.Model.Request;
using komatsu.api.Model.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace komatsu.api.Repo
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly IOptions<JwtConfig> _jwtConfig;
        private readonly devkomatsuContext _context;
        private readonly IEmailServiceRepo _emailService;
        public AuthenticationRepo(IOptions<JwtConfig> jwtConfig, devkomatsuContext context, IEmailServiceRepo emailService)
        {
            _jwtConfig = jwtConfig;
            _context = context;
            _emailService = emailService;
        }

        private readonly IDictionary<string, string> user = new Dictionary<string, string> { { "test", "test" }, { "test1", "test1" } };

        public async Task<string> Authenticate(string email, string password)
        { 
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            //var tokenKey = Encoding.ASCII.GetBytes(_jwtConfig.Value.Secret);
            var tokenKey = Encoding.ASCII.GetBytes("ogknsroodqdgwgxbxclydkpqnojyycdg");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.Name, user.Email)
                    new Claim(ClaimTypes.Name, "gmail")
                }),
                Expires = DateTime.Now.AddDays(Convert.ToDouble(30)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            LoginResponse obj = new LoginResponse();

            var user = await _context.User.Where(e => e.Email == email && e.Password == ComputeSha256Hash(password) && e.IsDelete == 0).FirstOrDefaultAsync();

            if (user == null)
            {
                obj.Message = "Username or password is not correct.";
                obj.Success = false;

                return obj;
            }

            if (user.IsActive == 0)
            {
                obj.Message = "User is not approved. Please contact application admin.";
                obj.Success = false;

                return obj;
            }

            if (user.IsLogin == 1)
            {
                obj.Message = "This user is active. Please login again.";
                obj.Success = false;

                return obj;
            }

            var jwtToken = await Authenticate(email, password);

            if (user != null)
            {
                user.Token = jwtToken;
            }

            _context.User.Update(user);
            _context.SaveChanges();

            obj.Token = jwtToken;
            obj.Message = "Login successfully.";
            obj.Success = true;

            return obj;
        }


        public async Task<BaseResponse<bool>> Register(RegisterRequest model)
        {
            BaseResponse<bool> obj = new BaseResponse<bool>();

            try
            {
                var userName = _context.User.Where(e => e.Email == model.Email).FirstOrDefault();

                if (userName != null)
                {
                    obj.Message = "This username already exists.";
                    obj.Status = false;
                    return obj;
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Confirm password doesn't match the password.",
                        Status = false,
                    };
                }

                var user = new User()
                {
                    Email = model.Email,
                    Password = ComputeSha256Hash(model.Password),
                    EmployeeId = model.EmployeeID,
                    IsActive = 0,
                    IsDelete = 0,
                    CreatedAt = DateTime.Now
                };

                _context.User.Add(user);
                _context.SaveChanges();

                obj.Message = "Register successfully.";
                obj.Status = true;

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<BaseResponse<bool>> ForgetPassword(string email)
        {
            var user = await _context.User.Where(e => e.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return new BaseResponse<bool> 
                {
                    Message = "No user associated with email",
                    Status = false,
                };
            }

            var jwtToken = await Authenticate(email, "");

            return new BaseResponse<bool>
            {
                Message = "Send reset password to email, please your email for reset password.",
                Status = true,
            };
        }

        public async Task<BaseResponse<bool>> ResetPassword(RegisterRequest model)
        {
            var user = await _context.User.Where(e => e.Email == model.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return new BaseResponse<bool>
                {
                    Message = "No user associated with email",
                    Status = false,
                };
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new BaseResponse<bool>
                {
                    Message = "Confirm password doesn't match the password.",
                    Status = false,
                };
            }

            var resetPassResult = await ResetPasswordByEmail(model);

            if (!resetPassResult)
            {
                return new BaseResponse<bool>
                {
                    Message = "No user associated with email",
                    Status = false,
                };
            }

            return new BaseResponse<bool>
            {
                Message = "Reset password successfully.",
                Status = true,
            };
        }


        public async Task<bool> ResetPasswordByEmail(RegisterRequest user)
        {
            bool success = false;

            var result = _context.User.Where(e => e.Email == user.Email).FirstOrDefault();
            if (result != null)
            {
                result.Password = ComputeSha256Hash(user.Password);
                result.UpdatedAt = DateTime.Now;

                _context.User.Update(result);
                _context.SaveChanges();

                success = true;
            }
            return success;
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordRequest model, string token)
        {
            var jwtToken = token.Substring(7);

            var user = _context.User.Where(e => e.Token == jwtToken).FirstOrDefault();

            if (user == null)
            {
                return new BaseResponse<bool>
                {
                    Message = "User not found.",
                    Status = false,
                };
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return new BaseResponse<bool>
                {
                    Message = "Confirm password doesn't match the password.",
                    Status = false,
                };
            }

            var resetPassResult = await ChangePasswordByID(model ,user.Id);

            if (!resetPassResult)
            {
                return new BaseResponse<bool>
                {
                    Message = "Old password doesn't match the password.",
                    Status = false,
                };
            }

            return new BaseResponse<bool>
            {
                Message = "Change password successfully.",
                Status = true,
            };
        }

        public async Task<bool> ChangePasswordByID(ChangePasswordRequest model, int id)
        {
            bool success = false;

            var result = _context.User.Where(e => e.Id == id).FirstOrDefault();

            if (result.Password != ComputeSha256Hash(model.Password)) return false;

            if (result != null)
            {
                result.Password = ComputeSha256Hash(model.NewPassword);
                result.UpdatedAt = DateTime.Now;

                _context.User.Update(result);
                _context.SaveChanges();

                success = true;
            }
            return success;
        }

        public async Task<BaseResponse<bool>> Logout(string token)
        {
            var jwtToken = token.Substring(7);

            var user = await _context.User.Where(e => e.Token == jwtToken).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Token = null;

                _context.User.Update(user);
                _context.SaveChanges();

                return new BaseResponse<bool>
                {
                    Message = "Logout successfully.",
                    Status = true,
                };
            }

            return new BaseResponse<bool>
            {
                Message = "User not found.",
                Status = false,
            };
        }
    }
}
