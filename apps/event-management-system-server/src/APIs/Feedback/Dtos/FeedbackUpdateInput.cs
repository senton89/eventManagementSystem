namespace EventManagementSystem.APIs.Dtos;

public class FeedbackUpdateInput
{
    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Event { get; set; }

    public string? Id { get; set; }

    public int? Rating { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? User { get; set; }
}
