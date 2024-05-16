using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Bll.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<long> AddAsync(UserEntityV1 userEntityV1, CancellationToken token)
    {
        var id = await _userRepository.AddAsync(userEntityV1, token);

        return id;
    }

    public async Task<UserEntityV1> GetAsync(long id, CancellationToken token)
    {
        var userEntity = await _userRepository.GetAsync(id, token);

        return userEntity;
    }

    public async Task<UserEntityV1[]> GetAsync(UserFilter userFilter, CancellationToken token)
    {
        var userEntities = await _userRepository.GetAsync(userFilter, token);

        return userEntities;
    }

    public async Task RemoveAsync(long id, CancellationToken token)
    {
        await _userRepository.RemoveAsync(id, token);
    }

    public async Task UpdateAsync(long id, UserEntityV1 userEntityV1, CancellationToken token)
    {
        await _userRepository.UpdateAsync(id, userEntityV1, token);
    }
}