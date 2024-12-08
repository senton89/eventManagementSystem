using EventManagementSystem.Core.Enums;

namespace EventManagementSystem.APIs.Dtos;

public class ParticipantRegistrationCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Event? Event { get; set; }

    public string? Id { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
