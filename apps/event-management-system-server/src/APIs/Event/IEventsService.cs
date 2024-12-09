using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;

namespace EventManagementSystem.APIs;

public interface IEventsService
{
    /// <summary>
    /// Create one Event
    /// </summary>
    public Task<Event> CreateEvent(EventCreateInput eventDbModel);

    /// <summary>
    /// Delete one Event
    /// </summary>
    public Task DeleteEvent(EventWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Events
    /// </summary>
    public Task<List<Event>> Events(EventFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Event records
    /// </summary>
    public Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Event
    /// </summary>
    public Task<Event> Event(EventWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Event
    /// </summary>
    public Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Feedbacks records to Event
    /// </summary>
    public Task ConnectFeedbacks(
        EventWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Disconnect multiple Feedbacks records from Event
    /// </summary>
    public Task DisconnectFeedbacks(
        EventWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Find multiple Feedbacks records for Event
    /// </summary>
    public Task<List<Feedback>> FindFeedbacks(
        EventWhereUniqueInput uniqueId,
        FeedbackFindManyArgs FeedbackFindManyArgs
    );

    /// <summary>
    /// Update multiple Feedbacks records for Event
    /// </summary>
    public Task UpdateFeedbacks(
        EventWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Connect multiple Notifications records to Event
    /// </summary>
    public Task ConnectNotifications(
        EventWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Disconnect multiple Notifications records from Event
    /// </summary>
    public Task DisconnectNotifications(
        EventWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Find multiple Notifications records for Event
    /// </summary>
    public Task<List<Notification>> FindNotifications(
        EventWhereUniqueInput uniqueId,
        NotificationFindManyArgs NotificationFindManyArgs
    );

    /// <summary>
    /// Update multiple Notifications records for Event
    /// </summary>
    public Task UpdateNotifications(
        EventWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Connect multiple ParticipantRegistrations records to Event
    /// </summary>
    public Task ConnectParticipantRegistrations(
        EventWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    );

    /// <summary>
    /// Disconnect multiple ParticipantRegistrations records from Event
    /// </summary>
    public Task DisconnectParticipantRegistrations(
        EventWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    );

    /// <summary>
    /// Find multiple ParticipantRegistrations records for Event
    /// </summary>
    public Task<List<ParticipantRegistration>> FindParticipantRegistrations(
        EventWhereUniqueInput uniqueId,
        ParticipantRegistrationFindManyArgs ParticipantRegistrationFindManyArgs
    );

    /// <summary>
    /// Update multiple ParticipantRegistrations records for Event
    /// </summary>
    public Task UpdateParticipantRegistrations(
        EventWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    );

    /// <summary>
    /// Connect multiple Sessions records to Event
    /// </summary>
    public Task ConnectSessions(
        EventWhereUniqueInput uniqueId,
        SessionWhereUniqueInput[] sessionsId
    );

    /// <summary>
    /// Disconnect multiple Sessions records from Event
    /// </summary>
    public Task DisconnectSessions(
        EventWhereUniqueInput uniqueId,
        SessionWhereUniqueInput[] sessionsId
    );

    /// <summary>
    /// Find multiple Sessions records for Event
    /// </summary>
    public Task<List<Session>> FindSessions(
        EventWhereUniqueInput uniqueId,
        SessionFindManyArgs SessionFindManyArgs
    );

    /// <summary>
    /// Update multiple Sessions records for Event
    /// </summary>
    public Task UpdateSessions(
        EventWhereUniqueInput uniqueId,
        SessionWhereUniqueInput[] sessionsId
    );
}
