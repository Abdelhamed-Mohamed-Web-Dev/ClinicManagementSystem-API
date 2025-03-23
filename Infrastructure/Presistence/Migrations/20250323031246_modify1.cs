using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modify1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lapTests_MedicalRecords_MedicalId",
                table: "lapTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lapTests",
                table: "lapTests");

            migrationBuilder.RenameTable(
                name: "lapTests",
                newName: "LapTests");

            migrationBuilder.RenameIndex(
                name: "IX_lapTests_MedicalId",
                table: "LapTests",
                newName: "IX_LapTests_MedicalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LapTests",
                table: "LapTests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LapTests_MedicalRecords_MedicalId",
                table: "LapTests",
                column: "MedicalId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LapTests_MedicalRecords_MedicalId",
                table: "LapTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LapTests",
                table: "LapTests");

            migrationBuilder.RenameTable(
                name: "LapTests",
                newName: "lapTests");

            migrationBuilder.RenameIndex(
                name: "IX_LapTests_MedicalId",
                table: "lapTests",
                newName: "IX_lapTests_MedicalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lapTests",
                table: "lapTests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lapTests_MedicalRecords_MedicalId",
                table: "lapTests",
                column: "MedicalId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
