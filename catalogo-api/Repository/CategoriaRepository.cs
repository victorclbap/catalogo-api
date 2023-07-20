using catalogo_api.Context;
using catalogo_api.Models;
using catalogo_api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Repository
{
    //Repository<Categoria> -> atenção à adição do tipo para acesso à entidade
    //O tipo é passado para a classe "pai" também
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {
        }
        public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriaParameters)
        {
            return await PagedList<Categoria>.ToPagedList(Get().OrderBy(c => c.Nome), categoriaParameters.PageNumber, categoriaParameters.PageSize);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return await Get().Include(c => c.Produtos).ToListAsync();
        }
    }
}
