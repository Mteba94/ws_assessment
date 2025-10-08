using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assessment.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addSentiments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sentiment",
                table: "Evaluations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "Evaluations");
        }
    }
}
