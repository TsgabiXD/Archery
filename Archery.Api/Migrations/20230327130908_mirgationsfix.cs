using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archery.Api.Migrations
{
    /// <inheritdoc />
    public partial class mirgationsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Target_Mapping_MappingId",
                table: "Target");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Mapping_MappingId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MappingId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Target_MappingId",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "MappingId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MappingId",
                table: "Target");

            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "Mapping",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Mapping",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_TargetId",
                table: "Mapping",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_UserId",
                table: "Mapping",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_Target_TargetId",
                table: "Mapping",
                column: "TargetId",
                principalTable: "Target",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_User_UserId",
                table: "Mapping",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mapping_Target_TargetId",
                table: "Mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_Mapping_User_UserId",
                table: "Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Mapping_TargetId",
                table: "Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Mapping_UserId",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Mapping");

            migrationBuilder.AddColumn<int>(
                name: "MappingId",
                table: "User",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MappingId",
                table: "Target",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "MappingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "MappingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "MappingId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_User_MappingId",
                table: "User",
                column: "MappingId");

            migrationBuilder.CreateIndex(
                name: "IX_Target_MappingId",
                table: "Target",
                column: "MappingId");

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
    }
}
