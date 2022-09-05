using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.DataAccess.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filtro = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ordemiento = null, string propiedadesIncluidas = "");

        TEntity GetFirstOrDefault(object id);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filtro);

        void Add(TEntity entidad);
        
        void Update(TEntity entidad);

        void Delete(object id);

        void Delete(TEntity borrar);
    }
}
