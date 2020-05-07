using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureGuessing.Models;
using Serilog;
using Serilog.Core;

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
            var game = await _context.Game.Include(g => g.Difficulty).FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        // GET: api/Games/5
        [HttpGet("{id}/{guess}")]
        public async Task<ActionResult<bool>> GetGame(Guid id, string guess)
        {
            var game = await _context.Game.FindAsync(id);

            if (game == null)
                return NotFound();

            if (game.isFinished)
                return Problem("Game already finished");

            if (guess.Trim().ToLower() == _context.Pictures.FindAsync(game.pictureID).Result.Answer.ToLower())
            {
                game.isFinished = true;
                game.Endtime = DateTime.Now;
                _context.Game.Update(game);
                await _context.SaveChangesAsync();
            }

            return Ok(game.isFinished);
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

                throw;
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GameStartResponse>> PostGame(GameStartObject gameStartObject)
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
            
            #region Select Picture
            Picture pic = null;
            string category;

            if (gameStartObject.category != null)
            {
                pic = _context.Pictures.OrderBy(o => Guid.NewGuid()).FirstOrDefaultAsync(p => p.Category.ToLower() == gameStartObject.category.ToLower()).Result;
            }
            if (pic == null)
            {
                pic = _context.Pictures.OrderBy(o => Guid.NewGuid()).FirstAsync().Result;
                category = "Default";
            }
            else category = pic.Category;

            Guid picid = pic.Id;
            #endregion
            
            Game game = new Game
            {
                Difficulty = bestMatchDifficulty,
                pictureID = picid,
                Category = category
            };
            await _context.Game.AddAsync(game);
            await _context.SaveChangesAsync();

            if (gameStartObject.category == null) 
                gameStartObject.category = "not selected";

            _logger.Information($"New Game, Selected diff and cat: {gameStartObject.difficultyScale}, {gameStartObject.category}; Real diff and cat {game.Difficulty.DifficultyScale}, {pic.Category}");
            
            var response = new GameStartResponse{Difficulty = game.Difficulty, Id = game.Id, pictureID = game.pictureID};
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, response);
        }

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
