using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Infrastructure.Models;

[Table("Feedbacks")]
public class FeedbackDbModel
{
    [StringLength(1000)]
    public string? Comment { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public EventDbModel? Event { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? Rating { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
