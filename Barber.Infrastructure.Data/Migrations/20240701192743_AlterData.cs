using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Infrastructure.Data.Migrations
{
    public partial class AlterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Schedule with one client. Client with one schedule",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_IdClient",
                table: "Schedules");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "Schedules",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_IdClient",
                table: "Schedules",
                column: "IdClient");

            migrationBuilder.AddForeignKey(
                name: "Schedule with one client. Client with many schedule",
                table: "Schedules",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Schedule with one client. Client with many schedule",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_IdClient",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_IdClient",
                table: "Schedules",
                column: "IdClient",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Schedule with one client. Client with one schedule",
                table: "Schedules",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
