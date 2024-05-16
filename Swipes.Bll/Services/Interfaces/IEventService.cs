using Swipes.Bll.Entities;
using Swipes.Bll.Models;

namespace Swipes.Bll.Services.Interfaces;

public interface IEventService
{
    Task<long> AddAsync(EventEntityV1 eventEntityV1, CancellationToken token);
    Task<EventEntityV1> GetAsync(long id, CancellationToken token);
    Task<EventEntityV1[]> GetAsync(EventFilter eventFilter, CancellationToken token);
    Task RemoveAsync(long id, CancellationToken token);
    Task UpdateAsync(long id, EventEntityV1 eventEntityV1, CancellationToken token);
}