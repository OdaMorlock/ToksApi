#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToksApi.Data;
using ToksApi.Models;

namespace ToksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueModelsController : ControllerBase
    {
        private readonly IssueDbContext _context;

        public IssueModelsController(IssueDbContext context)
        {
            _context = context;
        }

        // GET: api/IssueModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueModel>>> GetIssues()
        {
            return await _context.Issues.ToListAsync();
        }

        // GET: api/IssueModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IssueModel>> GetIssueModel(uint id)
        {
            var issueModel = await _context.Issues.FindAsync(id);

            if (issueModel == null)
            {
                return NotFound();
            }

            return issueModel;
        }

        // PUT: api/IssueModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssueModel(uint id, IssueModel issueModel)
        {
            if (id != issueModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(issueModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IssueModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IssueModel>> PostIssueModel(IssueModel issueModel)
        {
            _context.Issues.Add(issueModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIssueModel", new { id = issueModel.Id }, issueModel);
        }

        // DELETE: api/IssueModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueModel(uint id)
        {
            var issueModel = await _context.Issues.FindAsync(id);
            if (issueModel == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issueModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssueModelExists(uint id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
