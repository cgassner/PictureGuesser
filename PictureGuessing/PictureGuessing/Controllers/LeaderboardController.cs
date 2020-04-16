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
    public class LeaderboardController : ControllerBase
    {
        private readonly PictureGuessingDbContext _context;

        public LeaderboardController(PictureGuessingDbContext context)
        {
            _context = context;
        }

        // GET: api/Leaderboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardEntry>>> GetLeaderboardEntries()
        {
            return await _context.LeaderboardEntries.ToListAsync();
        }

        // GET: api/Leaderboard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaderboardEntry>> GetLeaderboardEntry(Guid id)
        {
            var leaderboardEntry = await _context.LeaderboardEntries.FindAsync(id);

            if (leaderboardEntry == null)
            {
                return NotFound();
            }

            return leaderboardEntry;
        }

        //// PUT: api/Leaderboard/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLeaderboardEntry(Guid id, LeaderboardEntry leaderboardEntry)
        //{
        //    if (id != leaderboardEntry.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(leaderboardEntry).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LeaderboardEntryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Leaderboard
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LeaderboardEntry>> PostLeaderboardEntry(LeaderboardEntryRequest entryData)
        {
            Game entryGame = _context.Game.FindAsync(entryData.gameId).Result;
            Picture entryPicture = _context.Pictures.FindAsync(entryGame.pictureID).Result;
            LeaderboardEntry entry = new LeaderboardEntry{
                Category = entryPicture.Category, 
                DifficultyScale = entryGame.Difficulty.DifficultyScale, 
                Name = entryData.playername, 
                TimeInSeconds = (entryGame.StartTime-entryGame.Endtime).TotalSeconds
            };
            await _context.LeaderboardEntries.AddAsync(entry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeaderboardEntry), new { id = entry.Id }, entry);
        }

        // DELETE: api/Leaderboard/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LeaderboardEntry>> DeleteLeaderboardEntry(Guid id)
        {
            var leaderboardEntry = await _context.LeaderboardEntries.FindAsync(id);
            if (leaderboardEntry == null)
            {
                return NotFound();
            }

            _context.LeaderboardEntries.Remove(leaderboardEntry);
            await _context.SaveChangesAsync();

            return leaderboardEntry;
        }

        private bool LeaderboardEntryExists(Guid id)
        {
            return _context.LeaderboardEntries.Any(e => e.Id == id);
        }
    }
}
