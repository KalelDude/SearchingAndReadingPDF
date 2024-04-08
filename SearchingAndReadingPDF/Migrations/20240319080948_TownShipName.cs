using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchingAndReadingPDF.Migrations
{
    /// <inheritdoc />
    public partial class TownShipName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TownshipNames",
                table: "TownShips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TownshipNames",
                table: "TownShips");
        }
    }
}
