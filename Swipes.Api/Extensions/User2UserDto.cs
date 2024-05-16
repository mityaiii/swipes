using Swipes.Api.Dto;
using Swipes.Bll.Entities;

namespace Swipes.Api.Extensions;

public static class User2UserDto
{
    public static UserDto ToDto(this UserEntityV1 userEntityV1)
    {
        return new UserDto
        {
            Id = userEntityV1.Id,
            Login = userEntityV1.Login,
            Name = userEntityV1.Name
        };
    }
}