using EventManagementSystem.APIs;
using EventManagementSystem.Infrastructure;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Extensions;
using EventManagementSystem.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.APIs;

public abstract class EventsServiceBase : IEventsService
{
    protected readonly EventManagementSystemDbContext _context;
    public EventsServiceBase(EventManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Event
    /// </summary>
    public async Task<Event> CreateEvent(EventCreateInput createDto)
    {
        var event = new EventDbModel
                    {
                CreatedAt = createDto.CreatedAt,
Date = createDto.Date,
Description = createDto.Description,
Location = createDto.Location,
Time = createDto.Time,
Title = createDto.Title,
UpdatedAt = createDto.UpdatedAt
};
      
            if (createDto.Id != null){
              event.Id = createDto.Id;
}
            if (createDto.Feedbacks != null)
              {
                  event.Feedbacks = await _context
                      .Feedbacks.Where(feedback => createDto.Feedbacks.Select(t => t.Id).Contains(feedback.Id))
                      .ToListAsync();
}

if (createDto.Notifications != null)
              {
                  event.Notifications = await _context
                      .Notifications.Where(notification => createDto.Notifications.Select(t => t.Id).Contains(notification.Id))
                      .ToListAsync();
}

if (createDto.ParticipantRegistrations != null)
              {
                  event.ParticipantRegistrations = await _context
                      .ParticipantRegistrations.Where(participantRegistration => createDto.ParticipantRegistrations.Select(t => t.Id).Contains(participantRegistration.Id))
                      .ToListAsync();
}

if (createDto.Sessions != null)
              {
                  event.Sessions = await _context
                      .Sessions.Where(session => createDto.Sessions.Select(t => t.Id).Contains(session.Id))
                      .ToListAsync();
}

_context.Events.Add(event);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<EventDbModel>(event.Id);
      
              if (result == null)
              {
    throw new NotFoundException();
}
      
              return result.ToDto();
}

/// <summary>
/// Delete one Event
/// </summary>
public async Task DeleteEvent(EventWhereUniqueInput uniqueId)
{
    var event = await _context.Events.FindAsync(uniqueId.Id);
    if (event == null)
        {
        throw new NotFoundException();
    }

    _context.Events.Remove(event);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Events
/// </summary>
public async Task<List<Event>> Events(EventFindManyArgs findManyArgs)
{
    var events = await _context
          .Events
  .Include(x => x.Sessions).Include(x => x.Feedbacks).Include(x => x.ParticipantRegistrations).Include(x => x.Notifications)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return events.ConvertAll(event => event.ToDto());
}

/// <summary>
/// Meta data about Event records
/// </summary>
public async Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs)
{

    var count = await _context
.Events
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one Event
/// </summary>
public async Task<Event> Event(EventWhereUniqueInput uniqueId)
{
    var events = await this.Events(
              new EventFindManyArgs { Where = new EventWhereInput { Id = uniqueId.Id } }
  );
    var event = events.FirstOrDefault();
    if (event == null)
      {
        throw new NotFoundException();
    }

    return event;
}

/// <summary>
/// Update one Event
/// </summary>
public async Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto)
{
    var event = updateDto.ToModel(uniqueId);

    if (updateDto.Feedbacks != null)
    {
                  event.Feedbacks = await _context
                      .Feedbacks.Where(feedback => updateDto.Feedbacks.Select(t => t).Contains(feedback.Id))
                      .ToListAsync();
    }

    if (updateDto.Notifications != null)
    {
                  event.Notifications = await _context
                      .Notifications.Where(notification => updateDto.Notifications.Select(t => t).Contains(notification.Id))
                      .ToListAsync();
    }

    if (updateDto.ParticipantRegistrations != null)
    {
                  event.ParticipantRegistrations = await _context
                      .ParticipantRegistrations.Where(participantRegistration => updateDto.ParticipantRegistrations.Select(t => t).Contains(participantRegistration.Id))
                      .ToListAsync();
    }

    if (updateDto.Sessions != null)
    {
                  event.Sessions = await _context
                      .Sessions.Where(session => updateDto.Sessions.Select(t => t).Contains(session.Id))
                      .ToListAsync();
    }

    _context.Entry(event).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Events.Any(e => e.Id == event.Id))
        {
            throw new NotFoundException();
        }
        else
        {
            throw;
        }
    }
}

/// <summary>
/// Connect multiple Feedbacks records to Event
/// </summary>
public async Task ConnectFeedbacks(EventWhereUniqueInput uniqueId, FeedbackWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Events.Include(x => x.Feedbacks)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Feedbacks.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Feedbacks);

    foreach (var child in childrenToConnect)
    {
        parent.Feedbacks.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple Feedbacks records from Event
/// </summary>
public async Task DisconnectFeedbacks(EventWhereUniqueInput uniqueId, FeedbackWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Events.Include(x => x.Feedbacks)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Feedbacks.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Feedbacks?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple Feedbacks records for Event
/// </summary>
public async Task<List<Feedback>> FindFeedbacks(EventWhereUniqueInput uniqueId, FeedbackFindManyArgs eventFindManyArgs)
{
    var feedbacks = await _context
          .Feedbacks
  .Where(m => m.EventId == uniqueId.Id)
  .ApplyWhere(eventFindManyArgs.Where)
  .ApplySkip(eventFindManyArgs.Skip)
  .ApplyTake(eventFindManyArgs.Take)
  .ApplyOrderBy(eventFindManyArgs.SortBy)
  .ToListAsync();

    return feedbacks.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple Feedbacks records for Event
/// </summary>
public async Task UpdateFeedbacks(EventWhereUniqueInput uniqueId, FeedbackWhereUniqueInput[] childrenIds)
{
    var event = await _context
            .Events.Include(t => t.Feedbacks)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    var children = await _context
      .Feedbacks.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }
  
      event.Feedbacks = children;
    await _context.SaveChangesAsync();
}

/// <summary>
/// Connect multiple Notifications records to Event
/// </summary>
public async Task ConnectNotifications(EventWhereUniqueInput uniqueId, NotificationWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Events.Include(x => x.Notifications)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Notifications.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Notifications);

    foreach (var child in childrenToConnect)
    {
        parent.Notifications.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple Notifications records from Event
/// </summary>
public async Task DisconnectNotifications(EventWhereUniqueInput uniqueId, NotificationWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Events.Include(x => x.Notifications)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Notifications.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Notifications?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple Notifications records for Event
/// </summary>
public async Task<List<Notification>> FindNotifications(EventWhereUniqueInput uniqueId, NotificationFindManyArgs eventFindManyArgs)
{
    var notifications = await _context
          .Notifications
  .Where(m => m.EventId == uniqueId.Id)
  .ApplyWhere(eventFindManyArgs.Where)
  .ApplySkip(eventFindManyArgs.Skip)
  .ApplyTake(eventFindManyArgs.Take)
  .ApplyOrderBy(eventFindManyArgs.SortBy)
  .ToListAsync();

    return notifications.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple Notifications records for Event
/// </summary>
public async Task UpdateNotifications(EventWhereUniqueInput uniqueId, NotificationWhereUniqueInput[] childrenIds)
{
    var event = await _context
            .Events.Include(t => t.Notifications)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    var children = await _context
      .Notifications.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }
  
      event.Notifications = children;
    await _context.SaveChangesAsync();
}

/// <summary>
/// Connect multiple ParticipantRegistrations records to Event
/// </summary>
public async Task ConnectParticipantRegistrations(EventWhereUniqueInput uniqueId, ParticipantRegistrationWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Events.Include(x => x.ParticipantRegistrations)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .ParticipantRegistrations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.ParticipantRegistrations);

    foreach (var child in childrenToConnect)
    {
        parent.ParticipantRegistrations.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple ParticipantRegistrations records from Event
/// </summary>
public async Task DisconnectParticipantRegistrations(EventWhereUniqueInput uniqueId, ParticipantRegistrationWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Events.Include(x => x.ParticipantRegistrations)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .ParticipantRegistrations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.ParticipantRegistrations?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple ParticipantRegistrations records for Event
/// </summary>
public async Task<List<ParticipantRegistration>> FindParticipantRegistrations(EventWhereUniqueInput uniqueId, ParticipantRegistrationFindManyArgs eventFindManyArgs)
{
    var participantRegistrations = await _context
          .ParticipantRegistrations
  .Where(m => m.EventId == uniqueId.Id)
  .ApplyWhere(eventFindManyArgs.Where)
  .ApplySkip(eventFindManyArgs.Skip)
  .ApplyTake(eventFindManyArgs.Take)
  .ApplyOrderBy(eventFindManyArgs.SortBy)
  .ToListAsync();

    return participantRegistrations.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple ParticipantRegistrations records for Event
/// </summary>
public async Task UpdateParticipantRegistrations(EventWhereUniqueInput uniqueId, ParticipantRegistrationWhereUniqueInput[] childrenIds)
{
    var event = await _context
            .Events.Include(t => t.ParticipantRegistrations)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    var children = await _context
      .ParticipantRegistrations.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }
  
      event.ParticipantRegistrations = children;
    await _context.SaveChangesAsync();
}

/// <summary>
/// Connect multiple Sessions records to Event
/// </summary>
public async Task ConnectSessions(EventWhereUniqueInput uniqueId, SessionWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Events.Include(x => x.Sessions)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Sessions.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Sessions);

    foreach (var child in childrenToConnect)
    {
        parent.Sessions.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple Sessions records from Event
/// </summary>
public async Task DisconnectSessions(EventWhereUniqueInput uniqueId, SessionWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Events.Include(x => x.Sessions)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Sessions.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Sessions?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple Sessions records for Event
/// </summary>
public async Task<List<Session>> FindSessions(EventWhereUniqueInput uniqueId, SessionFindManyArgs eventFindManyArgs)
{
    var sessions = await _context
          .Sessions
  .Where(m => m.EventId == uniqueId.Id)
  .ApplyWhere(eventFindManyArgs.Where)
  .ApplySkip(eventFindManyArgs.Skip)
  .ApplyTake(eventFindManyArgs.Take)
  .ApplyOrderBy(eventFindManyArgs.SortBy)
  .ToListAsync();

    return sessions.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple Sessions records for Event
/// </summary>
public async Task UpdateSessions(EventWhereUniqueInput uniqueId, SessionWhereUniqueInput[] childrenIds)
{
    var event = await _context
            .Events.Include(t => t.Sessions)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    var children = await _context
      .Sessions.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }
  
      event.Sessions = children;
    await _context.SaveChangesAsync();
}

}
