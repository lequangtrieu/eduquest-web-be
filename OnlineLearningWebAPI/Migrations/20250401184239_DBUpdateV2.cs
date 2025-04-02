using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "68e8bf8e-4c86-454e-b15f-9be6e3397466", "25f1ad3d-e58f-4c2c-8657-87f8599aa289" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e7ae8ded-2c3a-4219-9fd7-fc1ec23ac019", "35f85d80-3181-4cd6-822f-c2e17ba84792" });

            migrationBuilder.InsertData(
                table: "QuizType",
                columns: new[] { "quizTypeId", "typeName" },
                values: new object[] { 2, "Multiple" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuizType",
                keyColumn: "quizTypeId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5ae7c476-842c-460d-98f3-171bf58e02d8", "30771bee-0c21-47c3-ae91-1da3365346d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "58442058-d272-4f90-a7a1-5d69b36e0001", "e8c22976-b599-4b78-b1f1-3d6b244fc779" });

            migrationBuilder.InsertData(
                table: "QuizType",
                columns: new[] { "quizTypeId", "typeName" },
                values: new object[] { 1, "Single" });
        }
    }
}
