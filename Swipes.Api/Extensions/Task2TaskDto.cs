using Swipes.Api.Dto;
using Swipes.Bll.Entities;

namespace Swipes.Api.Extensions;

public static class Task2TaskDto
{
    public static TaskDto ToDto(this TaskEntityV1 taskEntityV1)
    {
        return new TaskDto
        {
            Id = taskEntityV1.Id,
            Description = taskEntityV1.Description,
            MapPointId = taskEntityV1.MapPointId,
            Name = taskEntityV1.Name,
            TaskType = taskEntityV1.Type,
        };
    }
}