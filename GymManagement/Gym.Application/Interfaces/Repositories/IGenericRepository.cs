using Gym.Domain.Common;
using System.Linq.Expressions;

namespace Gym.Application.Interfaces.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default);

    Task AddAsync(TEntity entity, CancellationToken ct = default);

    void Update(TEntity entity);
    void Delete(TEntity entity);

    Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
    Expression<Func<TEntity, bool>>? predicate,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
    int page,
    int pageSize,
    CancellationToken ct
);
}
