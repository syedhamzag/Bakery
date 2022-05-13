using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.DataTransferObject.DTOs.Role
{
    public class PostRole : BaseDTO
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
