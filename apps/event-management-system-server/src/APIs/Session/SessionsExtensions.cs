using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;

namespace EventManagementSystem.APIs.Extensions;

public static class SessionsExtensions
{
    public static Session ToDto(this SessionDbModel model)
    {
        return new Session
        {
            CreatedAt = model.CreatedAt,
            EndTime = model.EndTime,
            Event = model.EventId,
            Id = model.Id,
            Location = model.Location,
            StartTime = model.StartTime,
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SessionDbModel ToModel(
        this SessionUpdateInput updateDto,
        SessionWhereUniqueInput uniqueId
    )
    {
        var session = new SessionDbModel
        {
            Id = uniqueId.Id,
            EndTime = updateDto.EndTime,
            Location = updateDto.Location,
            StartTime = updateDto.StartTime,
            Title = updateDto.Title
        };

        if (updateDto.CreatedAt != null)
        {
            session.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Event != null)
        {
            session.EventId = updateDto.Event;
        }
        if (updateDto.UpdatedAt != null)
        {
            session.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return session;
    }
}
