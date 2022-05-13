using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.DataTransferObject.DTOs.Role
{
    public class GetRole : BaseDTO
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
