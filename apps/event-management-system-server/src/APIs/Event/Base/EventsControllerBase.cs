using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Common;

namespace EventManagementSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventsControllerBase : ControllerBase
{
    protected readonly IEventsService _service;
    public EventsControllerBase(IEventsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Event
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Event>> CreateEvent(EventCreateInput input)
    {
        var event = await _service.CreateEvent(input);
        
    return CreatedAtAction(nameof(Event), new { id = event.Id }, event); }

    /// <summary>
    /// Delete one Event
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEvent([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEvent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Events
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Event>>> Events([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.Events(filter));
    }

    /// <summary>
    /// Meta data about Event records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventsMeta([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.EventsMeta(filter));
    }

    /// <summary>
    /// Get one Event
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> Event([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Event(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Event
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEvent([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    EventUpdateInput eventUpdateDto)
    {
        try
        {
            await _service.UpdateEvent(uniqueId, eventUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Feedbacks records to Event
    /// </summary>
    [HttpPost("{Id}/feedbacks")]
    public async Task<ActionResult> ConnectFeedbacks([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    FeedbackWhereUniqueInput[] feedbacksId)
    {
        try
        {
            await _service.ConnectFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Feedbacks records from Event
    /// </summary>
    [HttpDelete("{Id}/feedbacks")]
    public async Task<ActionResult> DisconnectFeedbacks([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    FeedbackWhereUniqueInput[] feedbacksId)
    {
        try
        {
            await _service.DisconnectFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Feedbacks records for Event
    /// </summary>
    [HttpGet("{Id}/feedbacks")]
    public async Task<ActionResult<List<Feedback>>> FindFeedbacks([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    FeedbackFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindFeedbacks(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Feedbacks records for Event
    /// </summary>
    [HttpPatch("{Id}/feedbacks")]
    public async Task<ActionResult> UpdateFeedbacks([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    FeedbackWhereUniqueInput[] feedbacksId)
    {
        try
        {
            await _service.UpdateFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Notifications records to Event
    /// </summary>
    [HttpPost("{Id}/notifications")]
    public async Task<ActionResult> ConnectNotifications([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    NotificationWhereUniqueInput[] notificationsId)
    {
        try
        {
            await _service.ConnectNotifications(uniqueId, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Notifications records from Event
    /// </summary>
    [HttpDelete("{Id}/notifications")]
    public async Task<ActionResult> DisconnectNotifications([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    NotificationWhereUniqueInput[] notificationsId)
    {
        try
        {
            await _service.DisconnectNotifications(uniqueId, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Notifications records for Event
    /// </summary>
    [HttpGet("{Id}/notifications")]
    public async Task<ActionResult<List<Notification>>> FindNotifications([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    NotificationFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindNotifications(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Notifications records for Event
    /// </summary>
    [HttpPatch("{Id}/notifications")]
    public async Task<ActionResult> UpdateNotifications([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    NotificationWhereUniqueInput[] notificationsId)
    {
        try
        {
            await _service.UpdateNotifications(uniqueId, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple ParticipantRegistrations records to Event
    /// </summary>
    [HttpPost("{Id}/participantRegistrations")]
    public async Task<ActionResult> ConnectParticipantRegistrations([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId)
    {
        try
        {
            await _service.ConnectParticipantRegistrations(uniqueId, participantRegistrationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple ParticipantRegistrations records from Event
    /// </summary>
    [HttpDelete("{Id}/participantRegistrations")]
    public async Task<ActionResult> DisconnectParticipantRegistrations([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId)
    {
        try
        {
            await _service.DisconnectParticipantRegistrations(uniqueId, participantRegistrationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple ParticipantRegistrations records for Event
    /// </summary>
    [HttpGet("{Id}/participantRegistrations")]
    public async Task<ActionResult<List<ParticipantRegistration>>> FindParticipantRegistrations([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    ParticipantRegistrationFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindParticipantRegistrations(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple ParticipantRegistrations records for Event
    /// </summary>
    [HttpPatch("{Id}/participantRegistrations")]
    public async Task<ActionResult> UpdateParticipantRegistrations([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId)
    {
        try
        {
            await _service.UpdateParticipantRegistrations(uniqueId, participantRegistrationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Sessions records to Event
    /// </summary>
    [HttpPost("{Id}/sessions")]
    public async Task<ActionResult> ConnectSessions([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    SessionWhereUniqueInput[] sessionsId)
    {
        try
        {
            await _service.ConnectSessions(uniqueId, sessionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Sessions records from Event
    /// </summary>
    [HttpDelete("{Id}/sessions")]
    public async Task<ActionResult> DisconnectSessions([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    SessionWhereUniqueInput[] sessionsId)
    {
        try
        {
            await _service.DisconnectSessions(uniqueId, sessionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Sessions records for Event
    /// </summary>
    [HttpGet("{Id}/sessions")]
    public async Task<ActionResult<List<Session>>> FindSessions([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    SessionFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindSessions(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Sessions records for Event
    /// </summary>
    [HttpPatch("{Id}/sessions")]
    public async Task<ActionResult> UpdateSessions([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    SessionWhereUniqueInput[] sessionsId)
    {
        try
        {
            await _service.UpdateSessions(uniqueId, sessionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

}
