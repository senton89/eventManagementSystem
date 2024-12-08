namespace EventManagementSystem.APIs.Dtos;

public class SessionCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? EndTime { get; set; }

    public Event? Event { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public DateTime? StartTime { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
