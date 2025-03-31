using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExamTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "46aa2e9d-ad6e-43fd-93be-de827e613f50", "2b7c0f75-03b1-4bcc-ba78-06219a2591b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "71bff19b-ce20-4fad-a984-56bf1f3404a0", "f99e4902-8923-480f-9f98-46f9f7cd89fe" });

            migrationBuilder.InsertData(
                table: "QuizType",
                columns: new[] { "quizTypeId", "typeName" },
                values: new object[] { 1, "Single" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuizType",
                keyColumn: "quizTypeId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2e916360-1e3b-4d1d-ae45-70dd8f01933f", "bffb0e25-d43b-4284-9711-8942cf9d8d13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0d6ba687-27d3-4947-a14e-b9e481268b8d", "dd87cb75-636d-49e6-b0cc-5cd12658e961" });
        }
    }
}
