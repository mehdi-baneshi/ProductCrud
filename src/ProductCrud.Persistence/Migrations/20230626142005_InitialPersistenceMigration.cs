using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductCrud.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialPersistenceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ProduceDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    ManufacturePhone = table.Column<string>(type: "varchar(15)", nullable: true),
                    ManufactureEmail = table.Column<string>(type: "varchar(254)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedBy", "IsAvailable", "ManufactureEmail", "ManufacturePhone", "Name", "ProduceDate" },
                values: new object[,]
                {
                    { 1, "system", true, "info.us@breitling.com", "+989112223334", "Breitling Watch, Top Time B01 Triumph", new DateTime(2022, 6, 26, 17, 50, 5, 672, DateTimeKind.Local).AddTicks(9809) },
                    { 2, "system", false, "info.us@breitling.com", "+989112223334", "Breitling Watch, Top Time B01 Ford ThunderBird", new DateTime(2021, 6, 26, 17, 50, 5, 672, DateTimeKind.Local).AddTicks(9826) },
                    { 3, "admin", true, "customercare@us.alpinawathes.com", "+19994445556", "Alpiner Watch, Heritage Carrée Mechanical 140 Years", new DateTime(2020, 6, 26, 17, 50, 5, 672, DateTimeKind.Local).AddTicks(9828) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
