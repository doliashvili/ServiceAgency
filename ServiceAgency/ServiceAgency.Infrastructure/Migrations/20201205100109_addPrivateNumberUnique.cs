using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAgency.Infrastructure.Migrations
{
    public partial class addPrivateNumberUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Owner_PrivateNumber",
                table: "Owner",
                column: "PrivateNumber",
                unique: true,
                filter: "[PrivateNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owner_PrivateNumber",
                table: "Owner");
        }
    }
}
