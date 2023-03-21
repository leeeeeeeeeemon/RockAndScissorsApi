using System.ComponentModel.DataAnnotations;

namespace RockAndScissorsApi.Classes
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string CurrentPlayerName { get; set; }
        public string BoardState { get; set; }
        public bool IsGameOver { get; set; }
        public string WinnerName { get; set; }
    }
}
