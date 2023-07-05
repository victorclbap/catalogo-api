using catalogo_api.Models;

namespace catalogo_api.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //metodo especifido dos produtos
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
