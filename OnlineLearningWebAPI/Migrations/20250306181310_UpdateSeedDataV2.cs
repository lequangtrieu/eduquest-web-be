using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLearningWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreateDate", "Price" },
                values: new object[] { new DateOnly(2025, 3, 6), 10000 });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 2,
                columns: new[] { "CreateDate", "Price" },
                values: new object[] { new DateOnly(2025, 2, 4), 15000 });

            migrationBuilder.InsertData(
                table: "CourseCategory",
                columns: new[] { "categoryId", "name" },
                values: new object[,]
                {
                    { 3, "Web Development" },
                    { 4, "Data Science" },
                    { 5, "Cybersecurity" }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "courseId", "categoryId", "CourseTitle", "CreateDate", "Description", "ImageURL", "Price", "Status", "TeacherId" },
                values: new object[,]
                {
                    { 3, 3, "Building Modern Web Apps", new DateOnly(2025, 2, 19), "Develop dynamic websites using modern frameworks.", null, 20000, 0, "2" },
                    { 4, 4, "Data Analysis with Python", new DateOnly(2025, 1, 20), "Analyze and visualize data effectively.", null, 25000, 1, "2" },
                    { 5, 5, "Network Security Basics", new DateOnly(2025, 1, 5), "Learn the fundamentals of protecting systems.", null, 30000, 0, "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseCategory",
                keyColumn: "categoryId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "51e62d15-1d63-4aa5-8488-e228213a9246", "b98ad86a-95b4-4929-8542-271b22a66fed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b3fb4159-6b5b-4db2-8eb8-d7d76bc1159c", "93871200-934b-4d34-99bb-8cea16384f50" });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 1,
                columns: new[] { "CreateDate", "Price" },
                values: new object[] { new DateOnly(2025, 3, 1), 0 });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "courseId",
                keyValue: 2,
                columns: new[] { "CreateDate", "Price" },
                values: new object[] { new DateOnly(2025, 1, 30), 0 });
        }
    }
}
