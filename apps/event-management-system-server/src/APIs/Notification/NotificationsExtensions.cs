using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;

namespace EventManagementSystem.APIs.Extensions;

public static class NotificationsExtensions
{
    public static Notification ToDto(this NotificationDbModel model)
    {
        return new Notification
        {
            CreatedAt = model.CreatedAt,
            DateSent = model.DateSent,
            Event = model.EventId,
            Id = model.Id,
            Message = model.Message,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static NotificationDbModel ToModel(
        this NotificationUpdateInput updateDto,
        NotificationWhereUniqueInput uniqueId
    )
    {
        var notification = new NotificationDbModel
        {
            Id = uniqueId.Id,
            DateSent = updateDto.DateSent,
            Message = updateDto.Message
        };

        if (updateDto.CreatedAt != null)
        {
            notification.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Event != null)
        {
            notification.EventId = updateDto.Event;
        }
        if (updateDto.UpdatedAt != null)
        {
            notification.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            notification.UserId = updateDto.User;
        }

        return notification;
    }
}
