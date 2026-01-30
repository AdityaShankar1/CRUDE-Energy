using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crude.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CurrentReading", "LastUpdated", "MaxConsumptionThreshold", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0.0, new DateTime(2025, 12, 23, 14, 11, 35, 69, DateTimeKind.Utc).AddTicks(9950), 1000.0, "Main Transformer", "Healthy" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0.0, new DateTime(2025, 12, 23, 14, 11, 35, 70, DateTimeKind.Utc).AddTicks(400), 150.0, "Cooling Pump 01", "Healthy" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 0.0, new DateTime(2025, 12, 23, 14, 11, 35, 70, DateTimeKind.Utc).AddTicks(410), 600.0, "Heavy Drill Rig", "Healthy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
