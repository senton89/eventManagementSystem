using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[ApiController()]
public class NotificationsController : NotificationsControllerBase
{
    public NotificationsController(INotificationsService service)
        : base(service) { }
}
