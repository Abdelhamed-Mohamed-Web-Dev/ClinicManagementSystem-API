using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rateId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "_Rates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "_Rates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_rateId",
                table: "Appointments",
                column: "rateId",
                unique: true,
                filter: "[rateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments__Rates_rateId",
                table: "Appointments",
                column: "rateId",
                principalTable: "_Rates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments__Rates_rateId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_rateId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "rateId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "_Rates");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "_Rates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
