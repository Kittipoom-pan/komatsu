using System.ComponentModel.DataAnnotations;

namespace komatsu.api.Model.Request
{
    public class ChangePasswordRequest
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        //[Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
