using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchingAndReadingPDF.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentsTable");

            migrationBuilder.CreateTable(
                name: "PackageValuer",
                columns: table => new
                {
                    PackageValuerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValuerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageValuer", x => x.PackageValuerID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageValuer");

            migrationBuilder.CreateTable(
                name: "documentsTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectoryPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentsTable", x => x.ID);
                });
        }
    }
}
