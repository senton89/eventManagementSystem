namespace EventManagementSystem.APIs.Dtos;

public class EventWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public List<string>? Feedbacks { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public List<string>? Notifications { get; set; }

    public List<string>? ParticipantRegistrations { get; set; }

    public List<string>? Sessions { get; set; }

    public DateTime? Time { get; set; }

    public string? Title { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
