using Microsoft.AspNetCore.Mvc;
using Swipes.Api.Dto;
using Swipes.Api.Extensions;
using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Api.Controllers;

[ApiController]
[Route("/api/tasks")]
public class TaskController
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<long> AddAsync([FromBody] TaskEntityV1 taskEntityV1, CancellationToken token)
    {
        long taskId = await _taskService.AddAsync(taskEntityV1, token);

        return taskId;
    }

    [HttpGet("{id:long}")]
    public async Task<TaskDto> GetAsync([FromRoute] long id, CancellationToken token)
    {
        var taskEntity = await _taskService.GetAsync(id, token);

        return taskEntity.ToDto();
    }

    [HttpGet]
    public async Task<TaskDto[]> GetAsync([FromQuery] TaskFilter taskFilter, CancellationToken token)
    {
        var taskEntities = await _taskService.GetAsync(taskFilter, token);

        return taskEntities.Select(t => t.ToDto())
            .ToArray();
    }

    [HttpPut("{id:long}")]
    public async Task UpdateAsync([FromRoute] long id, [FromBody] TaskEntityV1 taskEntityV1, CancellationToken token)
    {
        await _taskService.UpdateAsync(id, taskEntityV1, token);
    }

    [HttpDelete("{id:long}")]
    public async Task RemoveAsync([FromRoute] long id, CancellationToken token)
    {
        await _taskService.RemoveAsync(id, token);
    }
}
