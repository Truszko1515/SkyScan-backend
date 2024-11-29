using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class BoundingBoxDto
    {
        public string north { get; set; } = string.Empty;
        public string south { get; set; } = string.Empty;
        public string west { get; set; } = string.Empty;
        public string east { get; set; } = string.Empty;
    }
}
