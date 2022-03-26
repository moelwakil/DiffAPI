using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Models
{
    public enum DiffType
    {
        // Data is equal
        Equals,
        // Size of data is not equal
        SizeDoNotMatch,
        // Size of data is equal, but content is different
        ContentDoNotMatch
    }
}
