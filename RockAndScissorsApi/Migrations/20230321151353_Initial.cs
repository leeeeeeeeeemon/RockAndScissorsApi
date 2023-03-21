using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RockAndScissorsApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Player1Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Player2Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CurrentPlayerName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BoardState = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IsGameOver = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WinnerName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
