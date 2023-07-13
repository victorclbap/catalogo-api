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
            //return Get().OrderBy(on => on.Nome).Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            //    .Take(produtosParameters.PageSize).ToList();

            return PagedList<Produto>.ToPagedList(Get().OrderBy(p => p.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);

        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }
    }
}
