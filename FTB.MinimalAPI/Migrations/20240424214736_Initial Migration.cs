using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FTB.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobiles",
                columns: table => new
                {
                    AutomobileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AutomobileImage = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Score = table.Column<double>(type: "REAL", nullable: false),
                    Wins = table.Column<int>(type: "INTEGER", nullable: false),
                    Losses = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobiles", x => x.AutomobileName);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Car1Name = table.Column<string>(type: "TEXT", nullable: false),
                    Car2Name = table.Column<string>(type: "TEXT", nullable: false),
                    Winner = table.Column<string>(type: "TEXT", nullable: true),
                    Score = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobiles");

            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
