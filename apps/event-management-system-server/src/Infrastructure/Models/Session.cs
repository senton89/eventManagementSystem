using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Infrastructure.Models;

[Table("Sessions")]
public class SessionDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? EndTime { get; set; }

    public string? EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public EventDbModel? Event { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Location { get; set; }

    public DateTime? StartTime { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
