using Domain.Common;

namespace Application.Infrastructure.Contracts.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> GetById(Guid id);
    Task Create(T obj);
    void Update(T obj);
    Task SaveChanges();
}