using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Models
{
    public class DiffItem
    {
        // Offset of the difference
        public int Offset { get; set; }
        // Length of the difference
        public int Length { get; set; }
    }
}
