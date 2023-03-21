namespace RockAndScissorsApi.Contex
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using RockAndScissorsApi.Classes;
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        private readonly IConfiguration _config;
        public GameContext(DbContextOptions<GameContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //_config.GetValue<string>("")
            optionsBuilder.UseSqlite("Filename=mydatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Game>().ToTable("Blogs");
            //modelBuilder.Entity<Game>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.HasIndex(e => e.Title).IsUnique();
            //    entity.Property(e => e.DateTimeAdd).HasDefaultValueSql("CURRENT_TIMESTAMP");
            //});
            base.OnModelCreating(modelBuilder);
        }

       
    }
}
