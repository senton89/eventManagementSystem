using EventManagementSystem.Infrastructure;

namespace EventManagementSystem.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(EventManagementSystemDbContext context)
        : base(context) { }
}
