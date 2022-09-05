using Microsoft.EntityFrameworkCore;

namespace LastTest.DataAccess.Repository.UnitOfWork
{
    public interface IUnitOfWork<out TContext>
        where TContext : DbContext
    {
        TContext Context { get; }

        void CreateTransaction();

        void Commit();

        void Rollback();

        void Save();

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;
    }
}
