using DiffAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Services
{
    public interface IBinaryDiffService
    {
        // Gets the differences between 2 binary arrays and returns DiffOutput object which contains difference type and a list of differnces
        // Paramaters (leftData: left binary array, rightData: right binary array)
        // Return (DiffOutput object)
        public DiffOutput GetDataDifferences(byte[] leftData, byte[] rightData);
    }
}
