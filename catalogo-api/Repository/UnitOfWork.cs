using catalogo_api.Context;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {


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

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
