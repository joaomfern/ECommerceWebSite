using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.Migrations
{
    public partial class CarrinhoCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItens",
                columns: table => new
                {
                    CarrinhoCompraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShirtId = table.Column<int>(type: "int", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CarrinhoCompraID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompraItens", x => x.CarrinhoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraItens_Shirts_ShirtId",
                        column: x => x.ShirtId,
                        principalTable: "Shirts",
                        principalColumn: "ShirtId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraItens_ShirtId",
                table: "CarrinhoCompraItens",
                column: "ShirtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompraItens");
        }
    }
}
