using catalogo_api.Models;
using catalogo_api.Pagination;

namespace catalogo_api.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameter);
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}
