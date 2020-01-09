using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureGuessing.Models;

namespace PictureGuessing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly PictureGuessingDbContext _context;

        public DifficultiesController(PictureGuessingDbContext context)
        {
            _context = context;
        }

        // GET: api/Difficulties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Difficulty>>> GetDifficulties()
        {
            return await _context.Difficulties.ToListAsync();
        }

        // GET: api/Difficulties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Difficulty>> GetDifficulty(float id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return difficulty;
        }

        // POST: api/Difficulties
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Difficulty>> PostDifficulty(Difficulty difficulty)
        {
            _context.Difficulties.Add(difficulty);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DifficultyExists(difficulty.DifficultyScale))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetDifficulty), new { id = difficulty.DifficultyScale }, difficulty);
        }

        // DELETE: api/Difficulties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Difficulty>> DeleteDifficulty(float id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            _context.Difficulties.Remove(difficulty);
            await _context.SaveChangesAsync();

            return difficulty;
        }

        private bool DifficultyExists(float id)
        {
            return _context.Difficulties.Any(e => Math.Abs(e.DifficultyScale - id) < 0.01);
        }
    }
}
