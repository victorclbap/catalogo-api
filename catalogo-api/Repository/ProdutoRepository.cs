using catalogo_api.Context;
using catalogo_api.Models;
using catalogo_api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Repository
{
    //O tipo é passado para a classe "pai" também
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }


        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            //return Get().OrderBy(on => on.Nome).
            //Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            //    .Take(produtosParameters.PageSize).ToList();

            return await PagedList<Produto>.ToPagedList(Get().
                OrderBy(p => p.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);

        }

        // na programação assíncrona a thread não fica bloqueada até terminar, uma outra é 
        // liberada bara um outro processo se necessário
        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(p => p.Preco).ToListAsync();
        }
    }
}
