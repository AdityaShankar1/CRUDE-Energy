using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crude.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThresholdToAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxConsumptionThreshold",
                table: "Assets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxConsumptionThreshold",
                table: "Assets");
        }
    }
}
