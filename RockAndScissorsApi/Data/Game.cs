using System.ComponentModel.DataAnnotations;

namespace RockAndScissorsApi.Data
{
    public class Game
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Player1Name { get; set; }
        [MaxLength(50)]
        public string Player2Name { get; set; }
        [MaxLength(50)]
        public string CurrentPlayerName { get; set; }
        [MaxLength(50)]
        public string BoardState { get; set; }
        public bool IsGameOver { get; set; }
        [MaxLength(50)]
        public string WinnerName { get; set; }
    }
}
