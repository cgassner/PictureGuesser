using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PictureGuessing.Models;
using Serilog;
using Serilog.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PictureGuessing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly PictureGuessingDbContext _context;
        private readonly Logger _logger;


        public GamesController(PictureGuessingDbContext context)
        {
            _context = context;

            _logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            return await _context.Game.Include(g => g.Difficulty).ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            var game = await _context.Game.Include(g => g.Difficulty).FirstOrDefaultAsync(i => i.Id == id);

            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        // GET: api/Games/5
        [HttpGet("{id}/{guess}")]
        public async Task<ActionResult<Game>> GetGame(Guid id, string guess)
        {
            var game = await _context.Game.FindAsync(id);

            if (game == null)
                return NotFound();

            if (game.isFinished)
                return game;

            if (guess.Trim().ToLower() == _context.Pictures.FindAsync(game.pictureID).Result.Answer.ToLower())
                game.isFinished = true;

            _context.Game.Update(game);
            await _context.SaveChangesAsync();
            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(Guid id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameStartObject gameStartObject)
        {
            #region Get Difficulty with best matching difficultScale
            List<Difficulty> difficulties = _context.Difficulties.ToListAsync().Result;
            Difficulty bestMatchDifficulty = difficulties[0];
            float bestMatchDelta = Math.Abs(gameStartObject.difficultyScale-bestMatchDifficulty.DifficultyScale);
            for (int i = 1; i < difficulties.Count; i++)
            {
                float curDelta = Math.Abs(difficulties[i].DifficultyScale - gameStartObject.difficultyScale);
                if (curDelta < bestMatchDelta)
                {
                    bestMatchDifficulty = difficulties[i];
                    bestMatchDelta = curDelta;
                }
            }
            #endregion
            
            #region SelectPicture
            Picture pic = null;
            Guid picid;
            if (gameStartObject.category != null || gameStartObject.category != "")
                pic = _context.Pictures.OrderBy(o => Guid.NewGuid()).FirstOrDefault(p => p.Category.ToLower() == gameStartObject.category.ToLower());
            if (pic != null)
            {
                picid = pic.Id;
                Console.WriteLine("Category found");
            }
            else
            {
                picid = _context.Pictures.OrderBy(o => Guid.NewGuid()).First().Id;
                Console.WriteLine("Category not Found");
            }
            #endregion
            
            Game game = new Game
            {
                Difficulty = bestMatchDifficulty,
                pictureID = picid
            };
            _context.Game.Add(game);
            await _context.SaveChangesAsync();

            _logger.Information($"New Game Created from IP {HttpContext.Connection.RemoteIpAddress}");
            
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }
        //[HttpPost]
        //public async Task<ActionResult<Game>> PostGame(Game game)
        //{
        //    _context.Game.Add(game);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        //}

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(Guid id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }

        private bool GameExists(Guid id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
