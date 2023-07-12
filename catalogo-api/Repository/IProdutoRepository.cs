using catalogo_api.Models;
using catalogo_api.Pagination;

namespace catalogo_api.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameter);
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
