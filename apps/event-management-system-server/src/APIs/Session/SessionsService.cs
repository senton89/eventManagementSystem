using EventManagementSystem.Infrastructure;

namespace EventManagementSystem.APIs;

public class SessionsService : SessionsServiceBase
{
    public SessionsService(EventManagementSystemDbContext context)
        : base(context) { }
}
