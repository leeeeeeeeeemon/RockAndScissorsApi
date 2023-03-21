using Microsoft.AspNetCore.Mvc;
using RockAndScissorsApi.Data;

namespace RockAndScissorsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly GameDbContext _context;
        public GamesController (GameDbContext context)
        {
            _context = context;
        }
        [HttpPost("createGame/{PlayerName}")]
        public int CreateGame(string PlayerName)
        {
            var game = new Game
            {
                Player1Name = PlayerName,
                BoardState = "WaitingForSecondPlayer",
                CurrentPlayerName = PlayerName,
                Player2Name = "No one",
                WinnerName = "No one"
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return game.Id;
        }

        //[HttpPost("{gameId}/join/{PlayerName}")]
        //public IActionResult JoinGame(Guid gameId, string PlayerName)
        //{
        //    var playerId = _gameService.JoinGameAsync(gameId, PlayerName);
        //    return Ok(playerId);
        //}

        //[HttpGet("{gameId}")]
        //public IActionResult GetGame(Guid gameId)
        //{
        //    var game = _gameService.GetGameAsync(gameId);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(game);
        //}

        //[HttpPost("{gameId}/move/{row}/{column}/{PlayerName}")]
        //public IActionResult MakeMove(Guid gameId, int row, int column, string PlayerName)
        //{
        //    var result = _gameService.MakeMoveAsync(gameId, row, column, PlayerName);
        //    //if (!result.Success)
        //    //{
        //    //    return BadRequest(result.ErrorMessage);
        //    //}
        //    //return Ok(result.Game);
        //    return Ok("kk");
        //}

        //[HttpGet("{gameId}/winner")]
        //public IActionResult CheckWinner(Guid gameId)
        //{
        //    var winner = _gameService.CheckWinnerAsync(gameId);
        //    return Ok(winner);
        //}
    }
}
