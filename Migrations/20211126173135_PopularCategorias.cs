using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) " +
                "VALUES('Clubes','Material Desportivo de Clubes')");
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) " +
               "VALUES('Seleções','Material Desportivo de Seleções')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
