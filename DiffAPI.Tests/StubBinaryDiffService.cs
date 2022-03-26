using DiffAPI.Models;
using DiffAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffAPI.Tests
{
    // Fake implementation for difference service to be used in testing.
    class StubBinaryDiffService : IBinaryDiffService
    {
        public DiffOutput GetDataDifferences(byte[] leftData, byte[] rightData)
        {
            return new DiffOutput(DiffType.Equals.ToString());
        }
    }
}
