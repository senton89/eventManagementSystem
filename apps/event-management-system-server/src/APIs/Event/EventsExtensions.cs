using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;

namespace EventManagementSystem.APIs.Extensions;

public static class EventsExtensions
{
    public static Event ToDto(this EventDbModel model)
    {
        return new Event
        {
            CreatedAt = model.CreatedAt,
            Date = model.Date,
            Description = model.Description,
            Feedbacks = model.Feedbacks?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Location = model.Location,
            Notifications = model.Notifications?.Select(x => x.Id).ToList(),
            ParticipantRegistrations = model.ParticipantRegistrations?.Select(x => x.Id).ToList(),
            Sessions = model.Sessions?.Select(x => x.Id).ToList(),
            Time = model.Time,
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,

        };
    }

    public static EventDbModel ToModel(this EventUpdateInput updateDto, EventWhereUniqueInput uniqueId)
    {
        var event = new EventDbModel { 
               Id = uniqueId.Id,
Date = updateDto.Date,
Description = updateDto.Description,
Location = updateDto.Location,
Time = updateDto.Time,
Title = updateDto.Title
};

     if(updateDto.CreatedAt != null) {
     event.CreatedAt = updateDto.CreatedAt.Value;
}
if(updateDto.UpdatedAt != null) {
     event.UpdatedAt = updateDto.UpdatedAt.Value;
}

    return event; }

}
