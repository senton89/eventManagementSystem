using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
