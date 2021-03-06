using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoppelProject.Models;

namespace PoppelProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Round")]
    public class RoundController : Controller
    {
        private readonly appDbContext _context;

        public RoundController(appDbContext context)
        {
            _context = context;
        }

        // GET: api/Round
        [HttpGet]
        public IEnumerable<Round> GetRound()
        {
            return _context.Round;
        }

        // GET: api/Round/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRound([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var round = await _context.Round.SingleOrDefaultAsync(m => m.Id == id);

            if (round == null)
            {
                return NotFound();
            }

            return Ok(round);
        }

        // PUT: api/Round/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRound([FromRoute] int id, [FromBody] Round round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != round.Id)
            {
                return BadRequest();
            }

            _context.Entry(round).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundExists(id))
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

        // POST: api/Round
        [HttpPost]
        public async Task<IActionResult> PostRound([FromBody] Round round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Round.Add(round);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRound", new { id = round.Id }, round);
        }

        // DELETE: api/Round/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRound([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var round = await _context.Round.SingleOrDefaultAsync(m => m.Id == id);
            if (round == null)
            {
                return NotFound();
            }

            _context.Round.Remove(round);
            await _context.SaveChangesAsync();

            return Ok(round);
        }

        private bool RoundExists(int id)
        {
            return _context.Round.Any(e => e.Id == id);
        }
    }
}