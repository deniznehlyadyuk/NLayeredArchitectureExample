using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false),
                    Neighborhood = table.Column<string>(type: "text", nullable: false),
                    Boulevard = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    BuildingNo = table.Column<int>(type: "integer", nullable: false),
                    RoomNo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IdentityNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employees_addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employees_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patients_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doctors_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_permits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                name: "secretaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LandPhoneCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secretaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secretaries_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "physical_examinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Diagnosis = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_physical_examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_physical_examinations_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_physical_examinations_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservations_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "housekeeper_responsible_floors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HousekeeperId = table.Column<Guid>(type: "uuid", nullable: false),
                    FloorNo = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "secretary_doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SecretaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secretary_doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secretary_doctors_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_secretary_doctors_secretaries_SecretaryId",
                        column: x => x.SecretaryId,
                        principalTable: "secretaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_doctors_EmployeeId",
                table: "doctors",
                column: "EmployeeId",
                unique: true);

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
                name: "IX_employees_AddressId",
                table: "employees",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_PersonId",
                table: "employees",
                column: "PersonId",
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

            migrationBuilder.CreateIndex(
                name: "IX_patients_PersonId",
                table: "patients",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_persons_IdentityNumber",
                table: "persons",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_physical_examinations_DoctorId",
                table: "physical_examinations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_physical_examinations_PatientId",
                table: "physical_examinations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_DoctorId",
                table: "reservations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_PatientId",
                table: "reservations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_secretaries_EmployeeId",
                table: "secretaries",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_secretary_doctors_DoctorId",
                table: "secretary_doctors",
                column: "DoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_secretary_doctors_SecretaryId",
                table: "secretary_doctors",
                column: "SecretaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_permits");

            migrationBuilder.DropTable(
                name: "employee_salaries");

            migrationBuilder.DropTable(
                name: "housekeeper_responsible_floors");

            migrationBuilder.DropTable(
                name: "physical_examinations");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "secretary_doctors");

            migrationBuilder.DropTable(
                name: "housekeepers");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "secretaries");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
