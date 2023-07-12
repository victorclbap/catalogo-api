using catalogo_api.Context;
using catalogo_api.Models;
using catalogo_api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {
        }
        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriaParameters)
        {
            return PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.Nome), categoriaParameters.PageNumber, categoriaParameters.pagrSize);
        }

        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(c => c.Produtos);
        }
    }
}
