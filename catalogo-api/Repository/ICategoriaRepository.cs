using catalogo_api.Models;
using catalogo_api.Pagination;

namespace catalogo_api.Repository
{
    //está herdando da interface caategoria e adicionando novas "regras" no contrato
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriaParameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
