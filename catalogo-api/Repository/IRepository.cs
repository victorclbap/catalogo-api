using System.Linq.Expressions;

namespace catalogo_api.Repository
{
    // interface genéria comum a todos os repositórios
    //como é genérica precisa ter o <T>
    public interface IRepository<T>
    {
        // IEnumerable realiza o filtro no cliente, IQuerable no banco de dados
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
