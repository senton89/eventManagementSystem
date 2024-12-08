using EventManagementSystem.Infrastructure;

namespace EventManagementSystem.APIs;

public class EventsService : EventsServiceBase
{
    public EventsService(EventManagementSystemDbContext context)
        : base(context) { }
}
