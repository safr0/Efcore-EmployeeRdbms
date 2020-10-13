using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCoreConsole.Migrations
{
    public partial class CreateEmployeeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeList",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeList", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeManagerList",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false),
                    ManagerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeManagerList", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_EmployeeManagerList_EmployeeList_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeList",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeManagerList_EmployeeList_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "EmployeeList",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeManagerList_ManagerID",
                table: "EmployeeManagerList",
                column: "ManagerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeManagerList");

            migrationBuilder.DropTable(
                name: "EmployeeList");
        }
    }
}
