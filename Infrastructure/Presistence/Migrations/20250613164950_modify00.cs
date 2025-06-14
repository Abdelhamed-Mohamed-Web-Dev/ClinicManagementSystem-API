using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modify00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments__Rates_rateId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "_Rates");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_rateId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "rateId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rateId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "_Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Rates_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Rates_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_rateId",
                table: "Appointments",
                column: "rateId",
                unique: true,
                filter: "[rateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX__Rates_DoctorId",
                table: "_Rates",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX__Rates_PatientId",
                table: "_Rates",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments__Rates_rateId",
                table: "Appointments",
                column: "rateId",
                principalTable: "_Rates",
                principalColumn: "Id");
        }
    }
}
