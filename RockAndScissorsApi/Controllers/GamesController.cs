using Microsoft.AspNetCore.Mvc;
using RockAndScissorsApi.Classes;
using RockAndScissorsApi.Contex;
using RockAndScissorsApi.Interfaces;

namespace RockAndScissorsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {

        public GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        [HttpPost("createGame/{PlayerName}")]
        public IActionResult CreateGame(string PlayerName)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Player1Name = PlayerName,
                BoardState = "WaitingForSecondPlayer"
            };
            _сontext.Games.AddAsync(game);
            _сontext.SaveChangesAsync();
            return game.Id;
        }

        [HttpPost("{gameId}/join/{PlayerName}")]
        public IActionResult JoinGame(Guid gameId, string PlayerName)
        {
            var playerId = _gameService.JoinGameAsync(gameId, PlayerName);
            return Ok(playerId);
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGame(Guid gameId)
        {
            var game = _gameService.GetGameAsync(gameId);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost("{gameId}/move/{row}/{column}/{PlayerName}")]
        public IActionResult MakeMove(Guid gameId, int row, int column, string PlayerName)
        {
            var result = _gameService.MakeMoveAsync(gameId, row, column, PlayerName);
            //if (!result.Success)
            //{
            //    return BadRequest(result.ErrorMessage);
            //}
            //return Ok(result.Game);
            return Ok("kk");
        }

        [HttpGet("{gameId}/winner")]
        public IActionResult CheckWinner(Guid gameId)
        {
            var winner = _gameService.CheckWinnerAsync(gameId);
            return Ok(winner);
        }
    }
}
