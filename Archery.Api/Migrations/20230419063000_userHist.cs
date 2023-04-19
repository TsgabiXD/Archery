using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archery.Api.Migrations
{
    /// <inheritdoc />
    public partial class userHist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parcour",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "Hist",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hist",
                value: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Hist",
                value: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "Hist",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hist",
                table: "User");

            migrationBuilder.InsertData(
                table: "Parcour",
                columns: new[] { "Id", "AnimalNumber", "Location", "Name" },
                values: new object[] { 1, 30, "Kirchschlag", "Dinosaurier" });
        }
    }
}
