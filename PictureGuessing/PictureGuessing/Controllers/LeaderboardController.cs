using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureGuessing.Models;
using Serilog;
using Serilog.Core;

namespace PictureGuessing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly PictureGuessingDbContext _context;
        private readonly Logger _logger;
        public LeaderboardController(PictureGuessingDbContext context)
        {
            _context = context;

            _logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
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

        // GET: api/Leaderboard/Landscape
        [HttpGet("{category}")]
        public async Task<ActionResult<LeaderboardEntry>> GetLeaderboardEntry(string category)
        {

            List<LeaderboardEntry> leaderboardEntrys = _context.LeaderboardEntries
                                                        .Where(e => e.Category.ToLower() == category.ToLower())
                                                        .ToListAsync().Result;
            if (leaderboardEntrys.Count == 0)
            {
                return NotFound($"No Entrys with category {category} found");
            }

            return Ok(leaderboardEntrys);
        }

        // POST: api/Leaderboard
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LeaderboardEntry>> PostLeaderboardEntry(LeaderboardEntryRequest entryData)
        {
            Game entryGame = await _context.Game.Include(g => g.Difficulty)
                                                .FirstOrDefaultAsync(g => g.Id == entryData.gameId);
            if (entryGame == null)
                return NotFound("Game not found");

            if (!entryGame.isFinished)
                return Problem("Game not finished, can not create leaderboard entry for game with id:" + entryGame.Id);

            if (_context.LeaderboardEntries.FirstOrDefaultAsync(e => e.GameId == entryGame.Id).Result != null)
                return Conflict("Entry already exists");

            Picture entryPicture = _context.Pictures.FindAsync(entryGame.pictureID).Result;
            LeaderboardEntry entry = new LeaderboardEntry{
                GameId = entryGame.Id,
                DifficultyScale = entryGame.Difficulty.DifficultyScale, 
                Name = entryData.playername, 
                Category = entryGame.Category,
                TimeInSeconds = (entryGame.Endtime-entryGame.StartTime).TotalSeconds
            };
            await _context.LeaderboardEntries.AddAsync(entry);
            await _context.SaveChangesAsync();

            _logger.Information($"Leaderboard entry created with name {entryData.playername} for game {entryData.gameId}");

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
