using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLearningWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExamTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "ExamTest",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 1,
                column: "CreateDate",
                value: new DateOnly(2025, 4, 1));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 2,
                column: "CreateDate",
                value: new DateOnly(2025, 3, 2));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 3,
                column: "CreateDate",
                value: new DateOnly(2025, 3, 17));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 4,
                column: "CreateDate",
                value: new DateOnly(2025, 2, 15));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 5,
                column: "CreateDate",
                value: new DateOnly(2025, 1, 31));

            migrationBuilder.InsertData(
                table: "CourseCategory",
                columns: new[] { "categoryId", "name" },
                values: new object[,]
                {
                    { 6, "Machine Learning" },
                    { 7, "Cloud Computing" },
                    { 8, "Mobile Development" },
                    { 9, "Database Management" },
                    { 10, "DevOps" },
                    { 11, "Blockchain" },
                    { 12, "Internet of Things (IoT)" },
                    { 13, "UI/UX Design" },
                    { 14, "Game Development" },
                    { 15, "Networking" },
                    { 16, "Business Intelligence" },
                    { 17, "Virtual Reality (VR)" },
                    { 18, "Augmented Reality (AR)" },
                    { 19, "IT Project Management" },
                    { 20, "Agile & Scrum" }
                });

            migrationBuilder.InsertData(
                table: "QuizType",
                columns: new[] { "quizTypeId", "typeName" },
                values: new object[] { 1, "Single" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "QuizType",
                keyColumn: "quizTypeId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "ExamTest");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bbec6d68-4b6c-4ee2-8f22-dd21a61aea6a", "78056129-10b3-472b-9403-93697b43b97c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c7612d67-27b3-4bf7-9056-d508e1744506", "f6bf3329-62a6-423a-bb2b-f2d7ccd228b2" });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 1,
                column: "CreateDate",
                value: new DateOnly(2025, 3, 6));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 2,
                column: "CreateDate",
                value: new DateOnly(2025, 2, 4));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 3,
                column: "CreateDate",
                value: new DateOnly(2025, 2, 19));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 4,
                column: "CreateDate",
                value: new DateOnly(2025, 1, 20));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 5,
                column: "CreateDate",
                value: new DateOnly(2025, 1, 5));
        }
    }
}
