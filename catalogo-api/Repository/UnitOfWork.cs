using catalogo_api.Context;

namespace catalogo_api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        // todos os repositórios partilham da mesma instância de dbcontext
        // é necessário adicionar como um serviço em program para utilizar

        private ProdutoRepository? _produtoRepository;
        private CategoriaRepository? _categoriaRepository;
        private readonly AppDbContext _context;

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {

            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);

            }
        }


        public UnitOfWork(AppDbContext contexto)
        {
            _context = contexto;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        // libera recursos do contexto
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
