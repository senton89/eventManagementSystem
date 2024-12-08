using EventManagementSystem.Core.Enums;

namespace EventManagementSystem.APIs.Dtos;

public class UserCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public List<Feedback>? Feedbacks { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public List<Notification>? Notifications { get; set; }

    public List<ParticipantRegistration>? ParticipantRegistrations { get; set; }

    public string Password { get; set; }

    public string? RecoveryToken { get; set; }

    public RoleEnum? Role { get; set; }

    public string Roles { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Username { get; set; }
}
