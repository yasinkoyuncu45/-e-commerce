using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketHup.Migrations
{
    public partial class AddAboutUsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    FormID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperScript = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtext = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experiencetext = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OurWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.FormID);
                });

            migrationBuilder.CreateTable(
                name: "Sp_Searches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kategori = table.Column<int>(type: "int", nullable: false),
                    URUN = table.Column<int>(type: "int", nullable: false),
                    MARKA = table.Column<int>(type: "int", nullable: false),
                    ARAMAISMI = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sp_Searches", x => x.ID);
                });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "Sp_Searches");

            migrationBuilder.DropTable(
                name: "Vw_MyOrders");
        }
    }
}
