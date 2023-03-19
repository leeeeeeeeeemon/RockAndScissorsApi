namespace RockAndScissorsApi.Contex
{
    using Microsoft.EntityFrameworkCore;
    using RockAndScissorsApi.Classes;
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
    }
}
