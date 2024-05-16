using Swipes.Bll.Entities.Enums;

namespace Swipes.Bll.Entities;

public record TaskEntityV1(long Id, 
    long MapPointId, 
    string Name, 
    TaskType Type, 
    string Description);