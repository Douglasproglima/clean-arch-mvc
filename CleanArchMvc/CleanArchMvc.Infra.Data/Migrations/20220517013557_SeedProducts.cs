using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Cabo 10mm','Kit Cabo de Vela 10 mm',275.56,4,'cabo_vela1.jpg',1)");

            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Velas Iridium NGK','Velas Iridium NGK',395.65,10,'velas.jpg',2)");

            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Cabo Velas GM','Kit Cabo Velas GM 8mm',125,80,'velas_gm.jpg',1)");

            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Filtro Inflow','Filtro de Ar Inflow',498.39,4,'filtro_inflow.jpg',3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
