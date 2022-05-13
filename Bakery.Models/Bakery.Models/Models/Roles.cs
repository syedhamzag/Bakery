using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Model.Models
{
    public class Roles : Base.BaseModel
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Users> User { get; set; } = new HashSet<Users>();
    }
}
