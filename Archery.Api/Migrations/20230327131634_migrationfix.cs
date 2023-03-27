using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archery.Api.Migrations
{
    /// <inheritdoc />
    public partial class migrationfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mapping_Target_TargetId",
                table: "Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Mapping_TargetId",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Mapping");

            migrationBuilder.AddColumn<int>(
                name: "MappingId",
                table: "Target",
                type: "INTEGER",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Target_Mapping_MappingId",
                table: "Target");

            migrationBuilder.DropIndex(
                name: "IX_Target_MappingId",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "MappingId",
                table: "Target");

            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "Mapping",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_TargetId",
                table: "Mapping",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_Target_TargetId",
                table: "Mapping",
                column: "TargetId",
                principalTable: "Target",
                principalColumn: "Id");
        }
    }
}
