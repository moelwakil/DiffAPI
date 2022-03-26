using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Models
{
    public class DiffObject
    {
        // This class holds the data used in binary data comparison (Id, leftData & rightData)
        public int Id { get; set; }
        public byte[] LeftData { get; set; }
        public byte[] RightData { get; set; }

        public DiffObject(int id, byte[] leftData, byte[] rightData)
        {
            Id = id;
            LeftData = leftData;
            RightData = rightData;
        }
    }
}
