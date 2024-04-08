using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchingAndReadingPDF.Migrations
{
    /// <inheritdoc />
    public partial class Townships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TownShips",
                columns: table => new
                {
                    TOWNSHIPID = table.Column<int>(type: "int", nullable: false)
                        ,
                    TOWN_NAME_DESC = table.Column<string>(type: "nvarchar(max)", nullable: false)
                }
               );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TownShips");
        }
    }
}
