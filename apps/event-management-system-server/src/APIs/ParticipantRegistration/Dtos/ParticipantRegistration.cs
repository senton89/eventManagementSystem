using EventManagementSystem.Core.Enums;

namespace EventManagementSystem.APIs.Dtos;

public class ParticipantRegistration
{
    public DateTime CreatedAt { get; set; }

    public string? Event { get; set; }

    public string Id { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? User { get; set; }
}
