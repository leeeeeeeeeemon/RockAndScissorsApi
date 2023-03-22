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

        [HttpPost("{gameId}/join/{PlayerName}")]
        public string JoinGame(int gameId, string PlayerName)
        {
            string answer;
            Game game = _context.Games.FirstOrDefault(g => g.Id == gameId);
            if (game != null)
            {
                if (game.Player1Name != PlayerName)
                {
                    if(game.Player2Name == "No one")
                    {
                        game.Player2Name = PlayerName;
                        game.BoardState = "- - - - - - - - -";
                        answer = "You are successfully connected";
                        _context.SaveChanges();
                    } else
                    {
                        answer = "There are already a maximum number of players in the game";
                    }
                }
                else
                {
                    answer = "The person with this nickname is already joined";
                }
            }
            else
            {
                answer = "A game with this id does not exist";
            }
            return (answer);
        }

        [HttpGet("{gameId}")]
        public string GetGame(int gameId)
        {
            string answer;
            var game = _context.Games.FirstOrDefault(g => g.Id == gameId);
            if (game == null)
            {
                answer = "Game not found";
            } else
            {
                answer = $"Player1Name: {game.Player1Name}, Player2Name: {game.Player2Name}, CurrentPlayerTurn: {game.CurrentPlayerName}, BoardState: {game.BoardState}, IsGameOver: {game.IsGameOver}";
            }
            return answer;
            
        }

        [HttpPost("{gameId}/move/{row}/{column}/{PlayerName}")]
        public string MakeMove(int gameId, int row, int column, string PlayerName)
        {
            string answer;
            Game game = _context.Games.FirstOrDefault(g => g.Id == gameId);
            if (game != null)
            {
                if(game.CurrentPlayerName == PlayerName)
                {
                    string[] gameBoard = game.BoardState.Split(new char[] { ' ' });
                    if (row > 0 && column > 0 && row <=3 && column <= 3)
                    {
                        int pos = (row - 1) * 3 + column;
                        if (gameBoard[pos] != "-")
                        {
                            if (game.CurrentPlayerName == game.Player1Name) gameBoard[pos] = "X";
                            else gameBoard[pos] = "O";
                            string resultBoard = "";
                            foreach (string board in gameBoard)
                            {
                                resultBoard += board;
                                resultBoard += " ";
                            }
                            game.BoardState = resultBoard;
                            game.CurrentPlayerName = game.Player2Name;
                            _context.SaveChanges();
                            answer = $"Success! BoardState: {game.BoardState}";
                        }
                        answer = "The field is taken";
                    }
                    else
                    {
                        answer = "Column or line is not properly entered";
                    }

                } else
                {
                    answer = "Its not your turn";
                }
            } else
            {
                answer = "A game with this id does not exist";
            }
            return answer;
        }

        //[HttpGet("{gameId}/winner")]
        //public IActionResult CheckWinner(Guid gameId)
        //{
        //    var winner = _gameService.CheckWinnerAsync(gameId);
        //    return Ok(winner);
        //}
    }
}
