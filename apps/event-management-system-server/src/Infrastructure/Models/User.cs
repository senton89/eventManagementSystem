using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventManagementSystem.Core.Enums;

namespace EventManagementSystem.Infrastructure.Models;

[Table("Users")]
public class UserDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public List<FeedbackDbModel>? Feedbacks { get; set; } = new List<FeedbackDbModel>();

    [StringLength(256)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(256)]
    public string? LastName { get; set; }

    public List<NotificationDbModel>? Notifications { get; set; } = new List<NotificationDbModel>();

    public List<ParticipantRegistrationDbModel>? ParticipantRegistrations { get; set; } =
        new List<ParticipantRegistrationDbModel>();

    [Required()]
    public string Password { get; set; }

    [StringLength(1000)]
    public string? RecoveryToken { get; set; }

    public RoleEnum? Role { get; set; }

    [Required()]
    public string Roles { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [Required()]
    public string Username { get; set; }
}
