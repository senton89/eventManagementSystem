namespace EventManagementSystem.APIs.Dtos;

public class FeedbackCreateInput
{
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public Event? Event { get; set; }

    public string? Id { get; set; }

    public int? Rating { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
