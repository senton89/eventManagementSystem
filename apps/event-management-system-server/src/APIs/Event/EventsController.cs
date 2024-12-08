using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[ApiController()]
public class EventsController : EventsControllerBase
{
    public EventsController(IEventsService service)
        : base(service) { }
}
