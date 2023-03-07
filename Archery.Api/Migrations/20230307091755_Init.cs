using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Archery.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parcour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AnimalNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Parcour",
                columns: new[] { "Id", "AnimalNumber", "Name" },
                values: new object[] { 1, 30, "Kirchschlag" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "NickName", "Role" },
                values: new object[,]
                {
                    { 1, "Tobias", "Schachner", "TsgabiXD", "Admin" },
                    { 2, "Luka", "Walkner", "woiges", "Admin" },
                    { 3, "Johannes", "Rölz", "JoRole", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcour");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
