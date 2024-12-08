namespace EventManagementSystem.APIs.Dtos;

public class NotificationUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? DateSent { get; set; }

    public string? Event { get; set; }

    public string? Id { get; set; }

    public string? Message { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? User { get; set; }
}
