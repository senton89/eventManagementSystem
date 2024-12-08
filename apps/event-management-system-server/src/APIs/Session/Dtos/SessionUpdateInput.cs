namespace EventManagementSystem.APIs.Dtos;

public class SessionUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Event { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public DateTime? StartTime { get; set; }

    public string? Title { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
