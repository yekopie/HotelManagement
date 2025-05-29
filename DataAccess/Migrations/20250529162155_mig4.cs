using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reservations_ReservationID",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "ReservationID",
                table: "Payments",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReservationID",
                table: "Payments",
                newName: "IX_Payments_ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Payments",
                newName: "ReservationID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReservationId",
                table: "Payments",
                newName: "IX_Payments_ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reservations_ReservationID",
                table: "Payments",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
