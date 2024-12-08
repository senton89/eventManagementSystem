using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Infrastructure.Models;

[Table("Events")]
public class EventDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public List<FeedbackDbModel>? Feedbacks { get; set; } = new List<FeedbackDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Location { get; set; }

    public List<NotificationDbModel>? Notifications { get; set; } = new List<NotificationDbModel>();

    public List<ParticipantRegistrationDbModel>? ParticipantRegistrations { get; set; } =
        new List<ParticipantRegistrationDbModel>();

    public List<SessionDbModel>? Sessions { get; set; } = new List<SessionDbModel>();

    public DateTime? Time { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
