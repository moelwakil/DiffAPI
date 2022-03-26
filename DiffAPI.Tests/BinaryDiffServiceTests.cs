using DiffAPI.Models;
using DiffAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DiffAPI.Tests
{
    public class BinaryDiffServiceTests
    {
        private BinaryDiffService _service;

        public BinaryDiffServiceTests()
        {
            _service = new BinaryDiffService();
        }

        [Fact]
        public void GetDataDifferences_Is_Equal_Data()
        {
            var result = _service.GetDataDifferences(new byte[] { 0, 1, 1, 0}, new byte[] { 0, 1, 1, 0 });
            Assert.Equal(DiffType.Equals.ToString(), result.DiffResultType);
        }

        [Fact]
        public void GetDataDifferences_Is_ContentDoNotMatch_Data()
        {
            var result = _service.GetDataDifferences(new byte[] { 0, 1, 1, 0 }, new byte[] { 1, 0, 1, 0 });
            Assert.Equal(DiffType.ContentDoNotMatch.ToString(), result.DiffResultType);
        }

        [Fact]
        public void GetDataDifferences_Is_SizeDoNotMatch_Data()
        {
            var result = _service.GetDataDifferences(new byte[] { 0, 1, 0 }, new byte[] { 1, 0, 1, 0 });
            Assert.Equal(DiffType.SizeDoNotMatch.ToString(), result.DiffResultType);
        }

    }
}
