using DiffAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffAPI.Services
{
    public class BinaryDiffService : IBinaryDiffService
    {
        
        public DiffOutput GetDataDifferences(byte[] leftData, byte[] rightData)
        {
            // Check if both binary arrays are equal
            if (leftData.SequenceEqual(rightData))
            {
                // return DiffOutput object with difference type of Equals
                return new DiffOutput(DiffType.Equals.ToString());
            }
            // Check if both binary arrays have different sizes
            else if (leftData.Length != rightData.Length)
            {
                // return DiffOutput object with difference type of SizeDoNotMatch
                return new DiffOutput(DiffType.SizeDoNotMatch.ToString());
            }
            // Both binary arrays have same size but content is different
            else
            {
                // Get the differences between binary arrays
                var diffs = GetDifferences(leftData, rightData);
                // return DiffOutput object with difference type of ContentDoNotMatch
                return new DiffOutput(DiffType.ContentDoNotMatch.ToString(), diffs);
            }
        }

        // Gets the differences between left & right binary arrays
        // Paramaters (leftData: left binary array, rightData: right binary array)
        // Return (list of DiffItem object containg each difference offset and length)
        // Algorithm: Loop through both arrays and do the following
        // 1- Check if content of the same index is the same
        // 2- In case of different content, increase the length with 1
        // 3- In case of same content, if the difference length is greater than 0 add DiffItem to the list and set length = 0 to start counting differences over again
        // 4- After the loop is done, check if the difference length is greater than 0, if so add DiffItem to the list
        private List<DiffItem> GetDifferences(byte[] leftData, byte[] rightData)
        {
            List<DiffItem> diffs = new List<DiffItem>();
            var length = 0;
            for (int i = 0; i < leftData.Length; i++)
            {
                if (leftData[i] == rightData[i])
                {
                    if (length > 0)
                    {
                        diffs.Add(new DiffItem
                        {
                            Offset = length - i,
                            Length = length
                        });
                        length = 0;
                    }
                }
                else
                {
                    length++;
                }
            }
            if (length > 0)
            {
                diffs.Add(new DiffItem
                {
                    Offset = leftData.Length - length,
                    Length = length
                });
            }
            return diffs;
        }
    }
}
