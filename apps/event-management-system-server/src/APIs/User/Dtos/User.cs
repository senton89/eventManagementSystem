using EventManagementSystem.Core.Enums;

namespace EventManagementSystem.APIs.Dtos;

public class User
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public List<string>? Feedbacks { get; set; }

    public string? FirstName { get; set; }

    public string Id { get; set; }

    public string? LastName { get; set; }

    public List<string>? Notifications { get; set; }

    public List<string>? ParticipantRegistrations { get; set; }

    public string Password { get; set; }

    public string? RecoveryToken { get; set; }

    public RoleEnum? Role { get; set; }

    public string Roles { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Username { get; set; }
}
