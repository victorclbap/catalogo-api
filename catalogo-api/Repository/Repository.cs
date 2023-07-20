using catalogo_api.Context;
using Microsoft.EntityFrameworkCore;
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
        //AsNoTracking() ganha desempenho, desabilitando rastreamento de entidades
        // IQuerable permite chamada assincrona, portanto não precisa adicionar Task
        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        // Expression<Func<T, bool> será usado uma expressão lambda validado pelo predicate
        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        // não modificam dados, apenas rastreiam alterações, somente o commit modifica os dados
        // e precisa aplicar programação assincrona

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
            _context.Entry(entity).State = EntityState.Modified;  // garante que o EF vai trackear a mudança na entidade
            _context.Set<T>().Update(entity);
        }
    }
}
