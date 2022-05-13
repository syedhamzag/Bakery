using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.DataTransferObject.DTOs.Users
{
    public class PostUser : BaseDTO
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Cell { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
