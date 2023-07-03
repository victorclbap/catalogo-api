using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace catalogo_api.Migrations
{
    /// <inheritdoc />
    public partial class populacategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {

            //inclui manualmente os dados, através da inserção de uma migration originalmente vazia
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values ('Bebidas', 'bebidas.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values ('Lanches', 'lanches.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

            // comando executado caso eu remova a migration
            mb.Sql("Delete from Categorias");
        }
    }
}
