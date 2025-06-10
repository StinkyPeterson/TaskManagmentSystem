using Domain.Common;

namespace Application.Infrastructure.Contracts;

public interface IRepository<T> where T : Entity
{
    Task<T> GetById(Guid id, CancellationToken token = default);
    Task Create(T obj, CancellationToken token = default);
    void Update(T obj);
    Task SaveChanges(CancellationToken token = default);
}