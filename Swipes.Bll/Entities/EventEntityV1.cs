using Swipes.Bll.Entities.Enums;

namespace Swipes.Bll.Entities;

public record EventEntityV1(long Id,
    string Name,
    long MapPointId,
    TaskType Type,
    string Description,
    DateTimeOffset StartAt,
    DateTimeOffset EndAt);