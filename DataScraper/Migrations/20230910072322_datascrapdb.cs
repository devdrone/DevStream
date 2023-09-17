using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataScraper.Migrations
{
    /// <inheritdoc />
    public partial class datascrapdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "moviedata",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    score = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    views = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imgurl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moviedata", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movielink",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fsize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    leech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    magnet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movielink", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "moviedata");

            migrationBuilder.DropTable(
                name: "movielink");
        }
    }
}
