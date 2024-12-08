using EventManagementSystem.Infrastructure;

namespace EventManagementSystem.APIs;

public class FeedbacksService : FeedbacksServiceBase
{
    public FeedbacksService(EventManagementSystemDbContext context)
        : base(context) { }
}
