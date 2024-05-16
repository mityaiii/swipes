using Swipes.Bll.Providers;
using Swipes.Dal.Exceptions;

namespace Swipes.Api.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(IDateTimeProvider dateTimeProvider, ILogger<ExceptionMiddleware> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            _logger.LogError($"[{_dateTimeProvider.Now()}]: {e.Message}");
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            _logger.LogError($"[{_dateTimeProvider.Now()}]: {e.Message}");
        }
    }
}
