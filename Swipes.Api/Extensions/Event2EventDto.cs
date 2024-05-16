using Swipes.Api.Dto;
using Swipes.Bll.Entities;

namespace Swipes.Api.Extensions;

public static class Event2EventDto
{
    public static EventDto ToDto(this EventEntityV1 eventEntityV1)
    {
        return new EventDto
        {
            Id = eventEntityV1.Id,
            Description = eventEntityV1.Description,
            StartAt = eventEntityV1.StartAt,
            EndAt = eventEntityV1.EndAt,
            MapPointId = eventEntityV1.MapPointId,
            TaskType = eventEntityV1.Type,
            Name = eventEntityV1.Name
        };
    }
}