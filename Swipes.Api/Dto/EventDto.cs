using Swipes.Bll.Entities.Enums;

namespace Swipes.Api.Dto;

public class EventDto
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public required TaskType TaskType { get; init; }
    public required long MapPointId { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset StartAt { get; init; }
    public required DateTimeOffset EndAt { get; init; }
}
