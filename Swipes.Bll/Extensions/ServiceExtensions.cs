using Microsoft.Extensions.DependencyInjection;
using Swipes.Bll.Providers;
using Swipes.Bll.Services;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Bll.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceExtensions(this IServiceCollection collection)
    {        
        AddProviders(collection);
        AddServices(collection);
        
        return collection;
    }

    private static void AddServices(IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<ITaskService, TaskService>();
        collection.AddScoped<IEventService, EventService>();
        // collection.AddScoped<ILobbyService, LobbyService>();
    }

    private static void AddProviders(IServiceCollection collection)
    {
        collection.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
}
