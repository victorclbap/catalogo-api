using catalogo_api.Context;
using catalogo_api.Models;
using catalogo_api.Pagination;

namespace catalogo_api.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }


        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            // return Get().OrderBy(p => p.Nome).Skip((produtosParameters.PageNumber - 1) * produtosParameters.pageSize).Take(produtosParameters.pageSize).ToList();
            return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);

        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }
    }
}
