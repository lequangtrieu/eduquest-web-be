using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdateV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ca92089c-02ec-4863-9915-7d43521683d9", "1fa6774a-21a7-4637-bd77-32c9df586f60" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "45e44a23-ee7a-45a6-b323-13489deef405", "9fb6d767-f8d1-4c1c-ae91-cd7d4d021561" });

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
                values: new object[] { "68e8bf8e-4c86-454e-b15f-9be6e3397466", "25f1ad3d-e58f-4c2c-8657-87f8599aa289" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e7ae8ded-2c3a-4219-9fd7-fc1ec23ac019", "35f85d80-3181-4cd6-822f-c2e17ba84792" });
        }
    }
}
