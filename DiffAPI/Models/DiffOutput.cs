using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Models
{
    public class DiffOutput
    {
        // Generic constructor
        public DiffOutput()
        {
        }

        // Constructor for equal and when sizes are not matched
        public DiffOutput(string diffType)
        {
            DiffResultType = diffType;
        }

        // Constructor in case of content does not matched
        public DiffOutput(string diffType, IEnumerable<DiffItem> differences)
        {
            DiffResultType = diffType;
            Diffs = differences;
        }
        // Difference type
        public string DiffResultType { get; set; }
        // List of differences
        public IEnumerable<DiffItem> Diffs { get; set; }
    }
}
