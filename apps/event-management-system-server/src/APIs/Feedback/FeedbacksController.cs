using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[ApiController()]
public class FeedbacksController : FeedbacksControllerBase
{
    public FeedbacksController(IFeedbacksService service)
        : base(service) { }
}
