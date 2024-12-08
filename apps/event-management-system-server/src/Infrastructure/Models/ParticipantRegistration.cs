using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventManagementSystem.Core.Enums;

namespace EventManagementSystem.Infrastructure.Models;

[Table("ParticipantRegistrations")]
public class ParticipantRegistrationDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public EventDbModel? Event { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
