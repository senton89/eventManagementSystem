namespace EventManagementSystem.APIs.Dtos;

public class EventCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public List<Feedback>? Feedbacks { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public List<Notification>? Notifications { get; set; }

    public List<ParticipantRegistration>? ParticipantRegistrations { get; set; }

    public List<Session>? Sessions { get; set; }

    public DateTime? Time { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
