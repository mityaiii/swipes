using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swipes.Bll.Repositories;
using Swipes.Dal.Infrastructure;
using Swipes.Dal.Repositories;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccessExtensions(this IServiceCollection collection)
    {
        AddPgRepositories(collection);

        return collection;
    }

    public static IServiceCollection AddDalInfrastructure(this IServiceCollection collection,
        IConfigurationRoot config)
    {
        collection.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));
        
        Postgres.MapCompositeTypes();
        Postgres.AddMigrations(collection);

        return collection;
    }
    
    private static void AddPgRepositories(IServiceCollection collection)
    {
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<ITaskRepository, TaskRepository>();
        collection.AddScoped<IEventRepository, EventRepository>();
        collection.AddScoped<IVoteRepository, VoteRepository>();
        collection.AddScoped<ILobbyRepository, LobbyRepository>();
    }
}
