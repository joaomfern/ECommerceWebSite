using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.Migrations
{
    public partial class PopularShirts1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
"VALUES('Camisola FC Porto','Camisola FC Porto Época 2021/2022, Equipamento Principal','Camisola FC Porto Época 2021/2022, Equipamento Principal',75,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/porto.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/porto.jpg',1,1,1)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
          "VALUES('Camisola Brasil','Camisola Seleção Brasileira Época 2021/2022, Equipamento Principal','Camisola Seleção Brasileira Época 2021/2022, Equipamento Principal',60,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/brasil.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/brasil.jpg',0,1,2)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
         "VALUES('Camisola Barcelona','Camisola Barcelona Época 2021/2022, Equipamento Principal','Camisola Barcelona Época 2021/2022, Equipamento Principal',80.05,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/barcelona.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/barcelona.jpg',0,1,1)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
        "VALUES('Camisola Boca Juniores','Camisola Boca Juniores Época 2021/2022, Equipamento Principal','Camisola Boca Juniores Época 2021/2022, Equipamento Principal',50,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/boca.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/boca.jpg',0,1,1)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
     "VALUES('Camisola Grêmio','Camisola Grêmio Época 2021/2022, Equipamento Principal','Camisola Grêmio Época 2021/2022, Equipamento Principal',40,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/gremio.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/gremio.jpg',0,1,1)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
"VALUES('Camisola Liverpool','Camisola Liverpool Época 2021/2022, Equipamento Principal','Camisola Liverpool Época 2021/2022, Equipamento Principal',80,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/liverpool.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/liverpool.jpg',0,1,1)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
"VALUES('Camisola Real Madrid','Camisola Real Madrid Época 2021/2022, Equipamento Principal','Camisola Real Madrid Época 2021/2022, Equipamento Principal',90,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/realmadrid.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/realmadrid.jpg',0,1,1)");

            migrationBuilder.Sql("INSERT INTO Shirts([Nome],[DescricaoCurta],[DescricaoDetalhada],[Preco],[ImagemUrl],[ImagemThumbnailUrl],[IsFavourite],[EmStock],[CategoriaId]) " +
       "VALUES('Camisola Portugal','Camisola Seleção Portuguesa Época 2021/2022, Equipamento Principal','Camisola Seleção Portuguesa Época 2021/2022, Equipamento Principal',60,'C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/portugal.jpg','C:/Users/jmigfernandes/Documents/EcommerceProject/EcommerceProject/EcommerceProject/wwwroot/images/portugal.jpg',1,1,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Shirts");
        }
    }
}
