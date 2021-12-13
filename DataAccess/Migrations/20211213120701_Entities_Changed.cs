using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Entities_Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_permits");

            migrationBuilder.DropTable(
                name: "employee_salaries");

            migrationBuilder.DropTable(
                name: "housekeeper_responsible_floors");

            migrationBuilder.DropTable(
                name: "housekeepers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee_permits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_permits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_permits_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_salaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_salaries_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "housekeepers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_housekeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_housekeepers_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "housekeeper_responsible_floors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FloorNo = table.Column<int>(type: "integer", nullable: false),
                    HousekeeperId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_housekeeper_responsible_floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_housekeeper_responsible_floors_housekeepers_HousekeeperId",
                        column: x => x.HousekeeperId,
                        principalTable: "housekeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_permits_EmployeeId",
                table: "employee_permits",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_salaries_EmployeeId",
                table: "employee_salaries",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_housekeeper_responsible_floors_HousekeeperId",
                table: "housekeeper_responsible_floors",
                column: "HousekeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_housekeepers_EmployeeId",
                table: "housekeepers",
                column: "EmployeeId",
                unique: true);
        }
    }
}
