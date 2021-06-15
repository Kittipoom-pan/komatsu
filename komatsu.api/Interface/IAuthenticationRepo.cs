using komatsu.api.Model.Request;
using komatsu.api.Model.Response;
using System.Threading.Tasks;

namespace komatsu.api.Interface
{
    public interface IAuthenticationRepo
    {
        Task<string> Authenticate(string email, string passeword);
        Task<LoginResponse> Login(string email, string passeword);
        Task<BaseResponse<bool>> Register(RegisterRequest user);
        Task<BaseResponse<bool>> ForgetPassword(string email);
        Task<BaseResponse<bool>> ResetPassword(RegisterRequest model);
        Task<BaseResponse<bool>> ChangePassword(ChangePasswordRequest model, string token);
        Task<bool> ResetPasswordByEmail(RegisterRequest user);
        Task<BaseResponse<bool>> Logout(string token);
    }
}
