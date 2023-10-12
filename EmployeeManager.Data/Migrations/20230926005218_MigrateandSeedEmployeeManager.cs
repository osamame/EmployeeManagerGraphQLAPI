using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrateandSeedEmployeeManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    MinimumSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DateCreated", "DepartmentLocation", "DepartmentName", "IsDeleted" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 9, 26, 1, 52, 18, 529, DateTimeKind.Local).AddTicks(7932), "Abuja", "Human Resource", false },
                    { 2L, new DateTime(2023, 9, 26, 1, 52, 18, 529, DateTimeKind.Local).AddTicks(7940), "Abuja", "Admin Department", false },
                    { 3L, new DateTime(2023, 9, 26, 1, 52, 18, 529, DateTimeKind.Local).AddTicks(7944), "Lagos", "Technical Department", false }
                });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "DateCreated", "Grade", "IsDeleted", "MaximumSalary", "MinimumSalary" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 9, 26, 1, 52, 18, 529, DateTimeKind.Local).AddTicks(7375), 1, false, 2000000000.99m, 1000000000.99m },
                    { 2L, new DateTime(2023, 9, 26, 1, 52, 18, 529, DateTimeKind.Local).AddTicks(7383), 2, false, 3000000000.99m, 2000000000.99m },
                    { 3L, new DateTime(2023, 9, 26, 1, 52, 18, 529, DateTimeKind.Local).AddTicks(7386), 3, false, 4000000000.99m, 3000000000.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Salaries");
        }
    }
}
