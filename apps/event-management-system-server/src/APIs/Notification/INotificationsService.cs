using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;

namespace EventManagementSystem.APIs;

public interface INotificationsService
{
    /// <summary>
    /// Create one Notification
    /// </summary>
    public Task<Notification> CreateNotification(NotificationCreateInput notification);

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public Task DeleteNotification(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Notifications
    /// </summary>
    public Task<List<Notification>> Notifications(NotificationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    public Task<MetadataDto> NotificationsMeta(NotificationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Notification
    /// </summary>
    public Task<Notification> Notification(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Notification
    /// </summary>
    public Task UpdateNotification(
        NotificationWhereUniqueInput uniqueId,
        NotificationUpdateInput updateDto
    );

    /// <summary>
    /// Get a event record for Notification
    /// </summary>
    public Task<Event> GetEvent(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a user record for Notification
    /// </summary>
    public Task<User> GetUser(NotificationWhereUniqueInput uniqueId);
}
