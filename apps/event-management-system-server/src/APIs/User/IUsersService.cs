using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;

namespace EventManagementSystem.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<User> CreateUser(UserCreateInput user);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<User>> Users(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<User> User(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Feedbacks records to User
    /// </summary>
    public Task ConnectFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Disconnect multiple Feedbacks records from User
    /// </summary>
    public Task DisconnectFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Find multiple Feedbacks records for User
    /// </summary>
    public Task<List<Feedback>> FindFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackFindManyArgs FeedbackFindManyArgs
    );

    /// <summary>
    /// Update multiple Feedbacks records for User
    /// </summary>
    public Task UpdateFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Connect multiple Notifications records to User
    /// </summary>
    public Task ConnectNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Disconnect multiple Notifications records from User
    /// </summary>
    public Task DisconnectNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Find multiple Notifications records for User
    /// </summary>
    public Task<List<Notification>> FindNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationFindManyArgs NotificationFindManyArgs
    );

    /// <summary>
    /// Update multiple Notifications records for User
    /// </summary>
    public Task UpdateNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Connect multiple ParticipantRegistrations records to User
    /// </summary>
    public Task ConnectParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    );

    /// <summary>
    /// Disconnect multiple ParticipantRegistrations records from User
    /// </summary>
    public Task DisconnectParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    );

    /// <summary>
    /// Find multiple ParticipantRegistrations records for User
    /// </summary>
    public Task<List<ParticipantRegistration>> FindParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationFindManyArgs ParticipantRegistrationFindManyArgs
    );

    /// <summary>
    /// Update multiple ParticipantRegistrations records for User
    /// </summary>
    public Task UpdateParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    );
}
