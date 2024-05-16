using Swipes.Bll.Entities.Enums;

namespace Swipes.Api.Dto;

public class TaskDto
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public required TaskType TaskType { get; init; }
    public required long MapPointId { get; init; }
    public required string Description { get; init; }
}