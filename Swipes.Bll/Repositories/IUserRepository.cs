using Swipes.Bll.Entities;
using Swipes.Bll.Models;

namespace Swipes.Bll.Repositories;

public interface IUserRepository
{
    Task<long> AddAsync(UserEntityV1 userEntityV1, CancellationToken token);
    Task<UserEntityV1> GetAsync(long id, CancellationToken token);
    Task<UserEntityV1[]> GetAsync(UserFilter userFilter, CancellationToken token);
    Task RemoveAsync(long id, CancellationToken token);
    Task UpdateAsync(long id, UserEntityV1 updatedUserEntityV1, CancellationToken token);
}