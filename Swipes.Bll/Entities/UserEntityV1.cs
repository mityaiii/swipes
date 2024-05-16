using Swipes.Bll.Entities.Enums;

namespace Swipes.Bll.Entities;

public record UserEntityV1(long Id,
    string Name,
    string Login,
    string? Password
    );