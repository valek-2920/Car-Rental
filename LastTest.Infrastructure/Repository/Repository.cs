using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace LastTest.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        public Repository(DbContext dbContext)
        {
            isDisposed = false;

            Context = dbContext;
            dbSet = Context.Set<TEntity>();
        }

        readonly DbContext Context;
        readonly DbSet<TEntity> dbSet;

        bool isDisposed;

        IQueryable<TEntity> Query
        {
            get { return dbSet; }
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filtro = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ordemiento = null, string propiedadesIncluidas = "")
        {
            IQueryable<TEntity> query = Query;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            foreach (var propiedad in propiedadesIncluidas.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(propiedad);
            }

            if (ordemiento != null)
            {
                return ordemiento(query).ToList();
            }

            return query.ToList();
        }

        public TEntity GetFirstOrDefault(object id)
        {
            return dbSet.Find(id);
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filtro)
        {
            return Query.FirstOrDefault(filtro);
        }

        public void Add(TEntity entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Add(entidad);
        }

        public void Update(TEntity entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Update(entidad);
        }

        public void Delete(object id)
        {
            TEntity entidad = GetFirstOrDefault(id);
            if (entidad == null)
            {
                throw new KeyNotFoundException($"{id}");
            }
            Delete(entidad);
        }

        public void Delete(TEntity entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Remove(entidad);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
