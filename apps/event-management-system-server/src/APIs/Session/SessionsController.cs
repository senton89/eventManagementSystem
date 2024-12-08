using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[ApiController()]
public class SessionsController : SessionsControllerBase
{
    public SessionsController(ISessionsService service)
        : base(service) { }
}
