using Microsoft.AspNetCore.Mvc;
using Swipes.Api.Dto;
using Swipes.Api.Extensions;
using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [HttpPost]
    public async Task<long> AddAsync([FromBody] EventEntityV1 eventEntityV1, CancellationToken token)
    {
        var eventId = await _eventService.AddAsync(eventEntityV1, token);

        return eventId;
    }

    [HttpGet("{id:long}")]
    public async Task<EventDto> GetAsync([FromRoute] long id, CancellationToken token)
    {
        var eventEntity = await _eventService.GetAsync(id, token);

        return eventEntity.ToDto();
    }

    [HttpGet]
    public async Task<EventDto[]> GetAsync([FromQuery] EventFilter eventFilter, CancellationToken token)
    {
        var eventEntities = await _eventService.GetAsync(eventFilter, token);

        return eventEntities.Select(e => e.ToDto())
            .ToArray();
    }

    [HttpDelete("{id:long}")]
    public async Task RemoveAsync([FromRoute] long id, CancellationToken token)
    {
        await _eventService.RemoveAsync(id, token);
    }

    [HttpPut("{id:long}")]
    public async Task UpdateAsync([FromRoute] long id, [FromBody] EventEntityV1 eventEntityV1, CancellationToken token)
    {
        await _eventService.UpdateAsync(id, eventEntityV1, token);
    }
}
