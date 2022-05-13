using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Model.Models
{
    public class Users : Base.BaseModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Cell { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public virtual Roles? Role { get; set; }
    }
}
