using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;

namespace EventManagementSystem.APIs.Extensions;

public static class ParticipantRegistrationsExtensions
{
    public static ParticipantRegistration ToDto(this ParticipantRegistrationDbModel model)
    {
        return new ParticipantRegistration
        {
            CreatedAt = model.CreatedAt,
            Event = model.EventId,
            Id = model.Id,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static ParticipantRegistrationDbModel ToModel(
        this ParticipantRegistrationUpdateInput updateDto,
        ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        var participantRegistration = new ParticipantRegistrationDbModel
        {
            Id = uniqueId.Id,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            participantRegistration.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Event != null)
        {
            participantRegistration.EventId = updateDto.Event;
        }
        if (updateDto.UpdatedAt != null)
        {
            participantRegistration.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            participantRegistration.UserId = updateDto.User;
        }

        return participantRegistration;
    }
}
