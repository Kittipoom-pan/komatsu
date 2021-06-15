using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace komatsu.api.DBContexts
{
    public partial class User
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public byte? IsActive { get; set; }
        public byte? IsLogin { get; set; }
        public byte? IsDelete { get; set; }
        public string Device { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
