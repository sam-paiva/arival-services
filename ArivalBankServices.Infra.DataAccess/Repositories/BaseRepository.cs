using ArivalBankServices.Core.Base;
using ArivalBankServices.Infra.DataAccess.Context;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ArivalBankServices.Infra.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<T> DbSet;

        public BaseRepository(IMongoContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<T>(typeof(T).Name);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            Context.AddCommand(() => DbSet.InsertOneAsync(entity, cancellationToken: cancellationToken));
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", entity.Id), entity));

            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", entity.Id)));

            return Task.CompletedTask;
        }

        public Task<T> GetByQueryAsync(Expression<Func<T, bool>>? filter)
        {
            var query = DbSet.AsQueryable().SingleOrDefault(filter!);

            return Task.FromResult(query)!;

        }

        public Task DeleteAsync(Guid id)
        {
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id)));

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter)
        {
            var query = DbSet.AsQueryable();
            if (filter is null)
                return Task.FromResult(query.AsEnumerable());

            return Task.FromResult(query.Where(filter!).AsEnumerable());
        }
    }
}
