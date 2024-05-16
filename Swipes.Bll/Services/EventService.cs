using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Repositories;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Bll.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<long> AddAsync(EventEntityV1 eventEntityV1, CancellationToken token)
    {
        long id = await _eventRepository.AddAsync(eventEntityV1, token);

        return id;
    }

    public async Task<EventEntityV1> GetAsync(long id, CancellationToken token)
    {
        var eventEntity = await _eventRepository.GetAsync(id, token);

        return eventEntity;
    }

    public async Task<EventEntityV1[]> GetAsync(EventFilter eventFilter, CancellationToken token)
    {
        var eventEntities = await _eventRepository.GetAsync(eventFilter, token);

        return eventEntities;
    }

    public async Task RemoveAsync(long id, CancellationToken token)
    {
        await _eventRepository.RemoveAsync(id, token);
    }

    public async Task UpdateAsync(long id, EventEntityV1 eventEntityV1, CancellationToken token)
    {
        await _eventRepository.UpdateAsync(id, eventEntityV1, token);
    }
}