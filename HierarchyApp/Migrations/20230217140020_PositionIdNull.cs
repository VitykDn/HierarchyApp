using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HierarchyApp.Migrations
{
    public partial class PositionIdNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_CompanyPositions_CompanyPositionId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyPositionId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_CompanyPositions_CompanyPositionId",
                table: "Employees",
                column: "CompanyPositionId",
                principalTable: "CompanyPositions",
                principalColumn: "CompanyPositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_CompanyPositions_CompanyPositionId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyPositionId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_CompanyPositions_CompanyPositionId",
                table: "Employees",
                column: "CompanyPositionId",
                principalTable: "CompanyPositions",
                principalColumn: "CompanyPositionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
