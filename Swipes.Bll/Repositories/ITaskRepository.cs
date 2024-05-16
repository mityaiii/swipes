using Swipes.Bll.Entities;
using Swipes.Bll.Models;

namespace Swipes.Bll.Repositories;

public interface ITaskRepository
{
    Task<long> AddAsync(TaskEntityV1 taskEntityV1, CancellationToken token);
    Task<TaskEntityV1> GetAsync(long id, CancellationToken token);
    Task<TaskEntityV1[]> GetAsync(TaskFilter taskFilter, CancellationToken token);
    Task RemoveAsync(long id, CancellationToken token);
    Task UpdateAsync(long id, TaskEntityV1 updatedTaskEntityV1, CancellationToken token);
}