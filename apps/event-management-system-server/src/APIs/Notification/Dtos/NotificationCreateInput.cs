namespace EventManagementSystem.APIs.Dtos;

public class NotificationCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? DateSent { get; set; }

    public Event? Event { get; set; }

    public string? Id { get; set; }

    public string? Message { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
