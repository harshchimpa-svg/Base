using Application.Interfaces.GenericRepositories;
using Domain.Common;

namespace Application.Interfaces.UnitOfWorkRepositories;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : class;
    Task<int> Save(CancellationToken cancellationToken);
    Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cachekeys);
    Task Rollback();
}
