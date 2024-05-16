using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Bll.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<long> AddAsync(TaskEntityV1 taskEntityV1, CancellationToken token)
    {
        long id = await _taskRepository.AddAsync(taskEntityV1, token);

        return id;
    }

    public async Task<TaskEntityV1> GetAsync(long id, CancellationToken token)
    {
        var task = await _taskRepository.GetAsync(id, token);

        return task;
    }

    public async Task<TaskEntityV1[]> GetAsync(TaskFilter taskFilter, CancellationToken token)
    {
        var tasks = await _taskRepository.GetAsync(taskFilter, token);

        return tasks;
    }

    public async Task RemoveAsync(long id, CancellationToken token)
    {
        await _taskRepository.RemoveAsync(id, token);
    }

    public async Task UpdateAsync(long id, TaskEntityV1 taskEntityV1, CancellationToken token)
    {
        await _taskRepository.UpdateAsync(id, taskEntityV1, token);
    }
}