using EventManagementSystem.Infrastructure;

namespace EventManagementSystem.APIs;

public class NotificationsService : NotificationsServiceBase
{
    public NotificationsService(EventManagementSystemDbContext context)
        : base(context) { }
}
