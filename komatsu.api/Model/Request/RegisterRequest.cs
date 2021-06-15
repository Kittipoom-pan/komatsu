namespace komatsu.api.Model.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        //[Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int EmployeeID { get; set; }
        public string Token { get; set; }
    }
}
