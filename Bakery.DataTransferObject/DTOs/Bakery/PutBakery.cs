using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DataTransferObject.DTOs.Bakery
{
    public class PutBakery : BaseDTO
    {
        public int BakeryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Contact { get; set; }
    }
}
