using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archery.Api.Migrations
{
    /// <inheritdoc />
    public partial class Mappingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Target_User_UserId",
                table: "Target");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Event_EventId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "User",
                newName: "MappingId");

            migrationBuilder.RenameIndex(
                name: "IX_User_EventId",
                table: "User",
                newName: "IX_User_MappingId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Target",
                newName: "MappingId");

            migrationBuilder.RenameIndex(
                name: "IX_Target_UserId",
                table: "Target",
                newName: "IX_Target_MappingId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Parcour",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mapping_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_EventId",
                table: "Mapping",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Target_Mapping_MappingId",
                table: "Target",
                column: "MappingId",
                principalTable: "Mapping",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Mapping_MappingId",
                table: "User",
                column: "MappingId",
                principalTable: "Mapping",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Target_Mapping_MappingId",
                table: "Target");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Mapping_MappingId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Mapping");

            migrationBuilder.RenameColumn(
                name: "MappingId",
                table: "User",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_User_MappingId",
                table: "User",
                newName: "IX_User_EventId");

            migrationBuilder.RenameColumn(
                name: "MappingId",
                table: "Target",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Target_MappingId",
                table: "Target",
                newName: "IX_Target_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Parcour",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AddForeignKey(
                name: "FK_Target_User_UserId",
                table: "Target",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Event_EventId",
                table: "User",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
