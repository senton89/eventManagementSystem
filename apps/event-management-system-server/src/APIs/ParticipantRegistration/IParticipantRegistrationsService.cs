using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;

namespace EventManagementSystem.APIs;

public interface IParticipantRegistrationsService
{
    /// <summary>
    /// Create one ParticipantRegistration
    /// </summary>
    public Task<ParticipantRegistration> CreateParticipantRegistration(
        ParticipantRegistrationCreateInput participantregistration
    );

    /// <summary>
    /// Delete one ParticipantRegistration
    /// </summary>
    public Task DeleteParticipantRegistration(ParticipantRegistrationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many ParticipantRegistrations
    /// </summary>
    public Task<List<ParticipantRegistration>> ParticipantRegistrations(
        ParticipantRegistrationFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about ParticipantRegistration records
    /// </summary>
    public Task<MetadataDto> ParticipantRegistrationsMeta(
        ParticipantRegistrationFindManyArgs findManyArgs
    );

    /// <summary>
    /// Get one ParticipantRegistration
    /// </summary>
    public Task<ParticipantRegistration> ParticipantRegistration(
        ParticipantRegistrationWhereUniqueInput uniqueId
    );

    /// <summary>
    /// Update one ParticipantRegistration
    /// </summary>
    public Task UpdateParticipantRegistration(
        ParticipantRegistrationWhereUniqueInput uniqueId,
        ParticipantRegistrationUpdateInput updateDto
    );

    /// <summary>
    /// Get a event record for ParticipantRegistration
    /// </summary>
    public Task<Event> GetEvent(ParticipantRegistrationWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a user record for ParticipantRegistration
    /// </summary>
    public Task<User> GetUser(ParticipantRegistrationWhereUniqueInput uniqueId);
}
