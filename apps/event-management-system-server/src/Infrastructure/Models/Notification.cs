using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Infrastructure.Models;

[Table("Notifications")]
public class NotificationDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? DateSent { get; set; }

    public string? EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public EventDbModel? Event { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Message { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
