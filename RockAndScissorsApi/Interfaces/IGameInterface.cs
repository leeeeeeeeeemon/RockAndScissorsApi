using Microsoft.EntityFrameworkCore;
using RockAndScissorsApi.Classes;
using RockAndScissorsApi.Contex;
namespace RockAndScissorsApi.Interfaces
{
    public interface IGameInterface
    {

        

        // Создать новую игру
        async Task<Guid> CreateGameAsync(string player1Name)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Player1Name = player1Name,
                BoardState = "WaitingForSecondPlayer"
            };
            //await DbContext.Games.AddAsync(game);
            //await _dbContext.SaveChangesAsync();
            return game.Id;
        }

        // Присоединиться к игре
        Task<bool> JoinGameAsync(Guid gameId, string player2Name);

        // Получить состояние игры
        Task<Game> GetGameAsync(Guid gameId);

        // Сделать ход
        Task<bool> MakeMoveAsync(Guid gameId, int row, int column, string playerName);

        // Проверить наличие победителя
        Task<string> CheckWinnerAsync(Guid gameId);
    }
}
