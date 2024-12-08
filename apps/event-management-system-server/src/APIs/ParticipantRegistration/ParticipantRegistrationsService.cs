using EventManagementSystem.Infrastructure;

namespace EventManagementSystem.APIs;

public class ParticipantRegistrationsService : ParticipantRegistrationsServiceBase
{
    public ParticipantRegistrationsService(EventManagementSystemDbContext context)
        : base(context) { }
}
