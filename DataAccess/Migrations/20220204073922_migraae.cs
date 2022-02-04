using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class migraae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "teachers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StudentLesson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentScoreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLesson_lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLesson_student_scores_StudentScoreId",
                        column: x => x.StudentScoreId,
                        principalTable: "student_scores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLesson_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLesson_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_LessonId",
                table: "teachers",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_LessonId",
                table: "StudentLesson",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_StudentId",
                table: "StudentLesson",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_StudentScoreId",
                table: "StudentLesson",
                column: "StudentScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_TeacherId",
                table: "StudentLesson",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_lessons_LessonId",
                table: "teachers",
                column: "LessonId",
                principalTable: "lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_lessons_LessonId",
                table: "teachers");

            migrationBuilder.DropTable(
                name: "StudentLesson");

            migrationBuilder.DropIndex(
                name: "IX_teachers_LessonId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "teachers");
        }
    }
}
