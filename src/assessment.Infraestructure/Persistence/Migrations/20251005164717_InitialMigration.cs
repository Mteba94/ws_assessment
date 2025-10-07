using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assessment.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Course = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.ProfessorId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationId);
                    table.ForeignKey(
                        name: "FK_Evaluations_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "ProfessorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorStats",
                columns: table => new
                {
                    ProfessorStatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalResponses = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    CompletionRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SemesterAverage = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorStats", x => x.ProfessorStatsId);
                    table.ForeignKey(
                        name: "FK_ProfessorStats_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "ProfessorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teaching = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Clarity = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Availability = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Fairness = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Overall = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingsId);
                    table.ForeignKey(
                        name: "FK_Ratings_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "ProfessorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationResponses",
                columns: table => new
                {
                    EvaluationResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    EvaluationId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationResponses", x => x.EvaluationResponseId);
                    table.ForeignKey(
                        name: "FK_EvaluationResponses_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResponses_EvaluationId",
                table: "EvaluationResponses",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResponses_QuestionId",
                table: "EvaluationResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ProfessorId",
                table: "Evaluations",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorStats_ProfessorId",
                table: "ProfessorStats",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProfessorId",
                table: "Ratings",
                column: "ProfessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationResponses");

            migrationBuilder.DropTable(
                name: "ProfessorStats");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}
