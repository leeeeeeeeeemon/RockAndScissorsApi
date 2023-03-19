using Microsoft.AspNetCore.Mvc;
using RockAndScissorsApi.Interfaces;

namespace RockAndScissorsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameInterface _gameService;

        public GamesController(IGameInterface gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("createGame/{PlayerName}")]
        public IActionResult CreateGame(string PlayerName)
        {
            var game = _gameService.CreateGameAsync(PlayerName);
            return Ok(game);
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
