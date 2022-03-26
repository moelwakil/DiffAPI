using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiffAPI.Models;
using System.Text.Json;
using System.Text;
using System.Net;
using DiffAPI.Services;

namespace DiffAPI.Controllers
{
    [Route("v1/diff")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        // DB context for InMemory database to hold the DiffObjects
        private readonly DiffContext _context;
        // Service that's used in spotting the differences if any and return the differences list or just returns Equal or SizeDoNotMatch
        private readonly IBinaryDiffService _binaryDiffService;
        public DiffController(DiffContext context, IBinaryDiffService binaryDiffService)
        {
            // Dependecy Injection
            _context = context;
            _binaryDiffService = binaryDiffService;
        }

        // GET: v1/diff/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiffData(int id)
        {
            // get the diff object from the InMemory database
            var diffData = await _context.DiffObjects.FindAsync(id);
            // If the object doesn't exist or one of it's arms (left or right) is null, return NotFound status
            if (diffData == null || diffData.LeftData == null || diffData.RightData == null)
                return NotFound();

            // if the object found, find the differenc type and return Ok status with details (diffResultType, diffs)
            return Ok(_binaryDiffService.GetDataDifferences(diffData.LeftData, diffData.RightData));
        }

        [Route("{Id}/left")]
        [HttpPut("{id}")]
        // Left endpoint to add the binary data to the left part
        public async Task<IActionResult> AddLeftData(int id, [FromBody] DiffContent data) {
            // if the data sent in the request is null, return BadRequest status
            if (data.Data == null)
                return BadRequest();
            
            // Check if DiffObject with the specified Id exists in the InMemory database
            DiffObject diffObject = await _context.DiffObjects.FindAsync(id);
            if (diffObject == null)
            {
                // If object doesn't exist add new object using the received Id and assign the data to leftData property
                diffObject = new DiffObject(id: id, leftData: data.Data, rightData: null);
                _context.DiffObjects.Add(diffObject);
            } else
            {
                // If object exists update leftData property
                diffObject.LeftData = data.Data;
            }
            await _context.SaveChangesAsync();

            // return created (201) status
            return CreatedAtRoute("", new { id = diffObject.Id });
        }

        [Route("{Id}/right")]
        [HttpPut("{id}")]
        // Right endpoint to add the binary data to the right part
        public async Task<IActionResult> AddRightData(int id, [FromBody] DiffContent data)
        {
            // if the data sent in the request is null, return BadRequest status
            if (data.Data == null)
                return BadRequest();

            // Check if DiffObject with the specified Id exists in the InMemory database
            var diffObject = await _context.DiffObjects.FindAsync(id);
            if (diffObject == null)
            {
                // If object doesn't exist add new object using the received Id and assign the data to rightData property
                diffObject = new DiffObject(id: id, leftData: null, rightData: data.Data);
                _context.DiffObjects.Add(diffObject);
            }
            else
            {
                // If object exists update rightData property
                diffObject.RightData = data.Data;
            }
            await _context.SaveChangesAsync();

            // return created (201) status
            return CreatedAtRoute("", new { id = diffObject.Id });
        }
    }
}
