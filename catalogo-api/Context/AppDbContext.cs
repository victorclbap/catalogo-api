using catalogo_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Context
{
    // é necessario herdar da classe db context
    // estabelece o relacionamento entre o banco de dados e as entidades
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //mapeia a entidade categoria para a tabela categorias, os atributos são as colunas
        public DbSet<Categoria>? Categorias { get; set; }

        public DbSet<Produto>? Produtos { get; set; }

    }
}
