using EventManagementSystem.APIs;
using EventManagementSystem.Infrastructure;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Extensions;
using EventManagementSystem.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.APIs;

public abstract class NotificationsServiceBase : INotificationsService
{
    protected readonly EventManagementSystemDbContext _context;
    public NotificationsServiceBase(EventManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Notification
    /// </summary>
    public async Task<Notification> CreateNotification(NotificationCreateInput createDto)
    {
        var notification = new NotificationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            DateSent = createDto.DateSent,
            Message = createDto.Message,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            notification.Id = createDto.Id;
        }
        if (createDto.Event != null)
        {
            notification.Event = await _context
                .Events.Where(event => createDto.Event.Id == event.Id)
                    .FirstOrDefaultAsync();
            }

if (createDto.User != null)
            {
                notification.User = await _context
                    .Users.Where(user => createDto.User.Id == user.Id)
                    .FirstOrDefaultAsync();
            }
  
              _context.Notifications.Add(notification);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<NotificationDbModel>(notification.Id);

if (result == null)
{
    throw new NotFoundException();
}

return result.ToDto();}

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public async Task DeleteNotification(NotificationWhereUniqueInput uniqueId)
{
    var notification = await _context.Notifications.FindAsync(uniqueId.Id);
    if (notification == null)
    {
        throw new NotFoundException();
    }

    _context.Notifications.Remove(notification);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Notifications
/// </summary>
public async Task<List<Notification>> Notifications(NotificationFindManyArgs findManyArgs)
{
    var notifications = await _context
          .Notifications
  .Include(x => x.Event).Include(x => x.User)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return notifications.ConvertAll(notification => notification.ToDto());
}

/// <summary>
/// Meta data about Notification records
/// </summary>
public async Task<MetadataDto> NotificationsMeta(NotificationFindManyArgs findManyArgs)
{

    var count = await _context
.Notifications
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one Notification
/// </summary>
public async Task<Notification> Notification(NotificationWhereUniqueInput uniqueId)
{
    var notifications = await this.Notifications(
              new NotificationFindManyArgs { Where = new NotificationWhereInput { Id = uniqueId.Id } }
  );
    var notification = notifications.FirstOrDefault();
    if (notification == null)
    {
        throw new NotFoundException();
    }

    return notification;
}

/// <summary>
/// Update one Notification
/// </summary>
public async Task UpdateNotification(NotificationWhereUniqueInput uniqueId, NotificationUpdateInput updateDto)
{
    var notification = updateDto.ToModel(uniqueId);

    if (updateDto.Event != null)
    {
        notification.Event = await _context
            .Events.Where(event => updateDto.Event == event.Id)
            .FirstOrDefaultAsync();
    }

    if (updateDto.User != null)
    {
        notification.User = await _context
            .Users.Where(user => updateDto.User == user.Id)
            .FirstOrDefaultAsync();
    }

    _context.Entry(notification).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Notifications.Any(e => e.Id == notification.Id))
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
/// Get a event record for Notification
/// </summary>
public async Task<Event> GetEvent(NotificationWhereUniqueInput uniqueId)
{
    var notification = await _context
          .Notifications.Where(notification => notification.Id == uniqueId.Id)
  .Include(notification => notification.Event)
  .FirstOrDefaultAsync();
    if (notification == null)
    {
        throw new NotFoundException();
    }
    return notification.Event.ToDto();
}

/// <summary>
/// Get a user record for Notification
/// </summary>
public async Task<User> GetUser(NotificationWhereUniqueInput uniqueId)
{
    var notification = await _context
          .Notifications.Where(notification => notification.Id == uniqueId.Id)
  .Include(notification => notification.User)
  .FirstOrDefaultAsync();
    if (notification == null)
    {
        throw new NotFoundException();
    }
    return notification.User.ToDto();
}

}
