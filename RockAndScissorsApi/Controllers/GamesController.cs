using Microsoft.AspNetCore.Mvc;
using RockAndScissorsApi.Data;
using System.Text.Json;

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
        public string CreateGame(string PlayerName)
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
            return JsonSerializer.Serialize(game);
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
                        answer = JsonSerializer.Serialize(game);
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
                answer = JsonSerializer.Serialize(game);
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
                if (game.IsGameOver == false)
                {
                    if (game.CurrentPlayerName == PlayerName)
                    {
                        if (game.Player2Name != "No one")
                        {
                            string[] gameBoard = game.BoardState.Split(new char[] { ' ' });
                            if (row > 0 && column > 0 && row <= 3 && column <= 3)
                            {
                                int pos = (row - 1) * 3 + column - 1;
                                if (gameBoard[pos] == "-")
                                {

                                    if (game.CurrentPlayerName == game.Player1Name) gameBoard[pos] = "X";
                                    else gameBoard[pos] = "O";
                                    string resultBoard = "";
                                    foreach (string board in gameBoard)
                                    {
                                        resultBoard += board;
                                        resultBoard += " ";
                                    }

                                    if (CheckWinner(gameBoard))
                                    {
                                        game.IsGameOver = true;
                                        game.WinnerName = game.CurrentPlayerName;
                                    }

                                    game.BoardState = resultBoard;

                                    if (game.CurrentPlayerName == game.Player1Name)
                                        game.CurrentPlayerName = game.Player2Name;
                                    else game.CurrentPlayerName = game.Player1Name;

                                    _context.SaveChanges();
                                    answer = JsonSerializer.Serialize(game);
                                    if (game.IsGameOver == true && game.WinnerName != "No one")
                                    {
                                        answer += $"The winner is {game.WinnerName}";
                                    }
                                    else if (game.IsGameOver == true)
                                    {
                                        answer += "Game over, draw.";
                                    }
                                } else
                                {
                                    answer = "The field is taken";
                                }
                            }
                            else
                            {
                                answer = "Column or line is not properly entered";
                            }
                        }
                        else
                        {
                            answer = "Wainting for second player";
                        }
                    } else
                    {
                        answer = "Its not your turn or You are not a member of the game";
                    }
                } else
                {
                    answer = "Game already end";
                }
            } else
            {
                answer = "A game with this id does not exist";
            }
            return answer;
        }

        private bool CheckWinner(string[] boardState)
        {
            bool IsWin = false;
            if ((boardState[0] == "X" && boardState[1] == "X" && boardState[2] == "X") ||
                ((boardState[3] == "X" && boardState[4] == "X" && boardState[5] == "X")) ||
                (boardState[6] == "X" && boardState[7] == "X" && boardState[8] == "X"))
                IsWin = true;
            if ((boardState[0] == "X" && boardState[3] == "X" && boardState[6] == "X") ||
                (boardState[1] == "X" && boardState[4] == "X" && boardState[7] == "X") ||
                (boardState[2] == "X" && boardState[5] == "X" && boardState[8] == "X"))
                IsWin = true;
            if ((boardState[0] == "X" && boardState[4] == "X" && boardState[8] == "X") ||
                (boardState[2] == "X" && boardState[4] == "X" && boardState[6] == "X"))
                IsWin = true;
            if ((boardState[0] == "O" && boardState[1] == "O" && boardState[2] == "O") ||
                ((boardState[3] == "O" && boardState[4] == "O" && boardState[5] == "O")) ||
                (boardState[6] == "O" && boardState[7] == "O" && boardState[8] == "O"))
                IsWin = true;
            if ((boardState[0] == "O" && boardState[3] == "O" && boardState[6] == "O") ||
                (boardState[1] == "O" && boardState[4] == "O" && boardState[7] == "O") ||
                (boardState[2] == "O" && boardState[5] == "O" && boardState[8] == "O"))
                IsWin = true;
            if ((boardState[0] == "O" && boardState[4] == "O" && boardState[8] == "O") ||
                (boardState[2] == "O" && boardState[4] == "O" && boardState[6] == "O"))
                IsWin = true;
            return IsWin;
        }

        [HttpGet("{gameId}/winner")]
        public string CheckWinner(int gameId)
        {
            string answer = "";
            var game = _context.Games.FirstOrDefault(g => g.Id == gameId);
            if (game == null) answer = "A game with this id does not exist";
            else if (game.IsGameOver == false) answer = "The game isn't over yet.";
            else answer = JsonSerializer.Serialize(game);
            return answer;
        }
    }
}
