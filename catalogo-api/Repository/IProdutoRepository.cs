using catalogo_api.Models;
using catalogo_api.Pagination;

namespace catalogo_api.Repository
{
    //está herdando da interface produto e adicionando novas "regras" no contrato
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameter);
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}
