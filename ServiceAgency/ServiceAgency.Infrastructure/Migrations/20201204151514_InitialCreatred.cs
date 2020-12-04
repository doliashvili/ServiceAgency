using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceAgency.Infrastructure.Migrations
{
    public partial class InitialCreatred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owner_Car_CarId",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Owner_CarId",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Owner");

            migrationBuilder.AddColumn<int>(
                name: "ActiveOwnerId",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarOwner",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    OwnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOwner", x => new { x.CarsId, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_CarOwner_Car_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarOwner_Owner_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarOwner_OwnersId",
                table: "CarOwner",
                column: "OwnersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarOwner");

            migrationBuilder.DropColumn(
                name: "ActiveOwnerId",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Owner",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Owner",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_CarId",
                table: "Owner",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Car_CarId",
                table: "Owner",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
