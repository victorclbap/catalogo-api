using catalogo_api.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace catalogo_api.Repository
{

    //classe genérica de repositório com uma interface genérica
    //pode adicionar outras interfaces conforme necessidade individual
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context { get; set; }

        public Repository(AppDbContext contexto)
        {
            _context = contexto;
        }

        // Set<T> indica uma instância do DbSet<T> para o acesso a entidade de determinado tipo de contexto
        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }


        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
