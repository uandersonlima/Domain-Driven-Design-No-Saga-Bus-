using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscoreStore.Catalog.Data.Migrations
{
    /// <inheritdoc />
    public partial class fillCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("0bc27c7f-44ae-49e7-bdbf-69487c2aac0b"), 3, "Relogio" },
                    { new Guid("2376014e-fa9e-4020-a710-674ca68ccd04"), 6, "Sapatos" },
                    { new Guid("358527ea-0e3d-4dc4-92c7-0dd95a011f03"), 5, "Banho" },
                    { new Guid("37f33546-4636-46b4-9be9-04d34855623b"), 1, "Camisa" },
                    { new Guid("bd6b53a8-9042-47ca-8456-46082a560936"), 4, "Celulares" },
                    { new Guid("e4b8e0b7-cc0f-4f57-8c8f-051ae0e39f2a"), 7, "Serviços" },
                    { new Guid("fdadfa58-06ee-4fc8-8cc4-a79c197e2197"), 2, "Caneca" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0bc27c7f-44ae-49e7-bdbf-69487c2aac0b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2376014e-fa9e-4020-a710-674ca68ccd04"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("358527ea-0e3d-4dc4-92c7-0dd95a011f03"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("37f33546-4636-46b4-9be9-04d34855623b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bd6b53a8-9042-47ca-8456-46082a560936"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e4b8e0b7-cc0f-4f57-8c8f-051ae0e39f2a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fdadfa58-06ee-4fc8-8cc4-a79c197e2197"));
        }
    }
}
