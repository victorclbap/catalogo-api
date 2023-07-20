using catalogo_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Context
{
    // Microsoft.AspNet.Identity.EntityFramework - pacote

    // estabelece o relacionamento entre o banco de dados e as entidades
    // é necessario herdar da classe db context ou identity db context qdo for o caso
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
