using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[ApiController()]
public class ParticipantRegistrationsController : ParticipantRegistrationsControllerBase
{
    public ParticipantRegistrationsController(IParticipantRegistrationsService service)
        : base(service) { }
}
