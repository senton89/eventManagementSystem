using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.Infrastructure.Models;

namespace EventManagementSystem.APIs.Extensions;

public static class FeedbacksExtensions
{
    public static Feedback ToDto(this FeedbackDbModel model)
    {
        return new Feedback
        {
            Comment = model.Comment,
            CreatedAt = model.CreatedAt,
            Event = model.EventId,
            Id = model.Id,
            Rating = model.Rating,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static FeedbackDbModel ToModel(
        this FeedbackUpdateInput updateDto,
        FeedbackWhereUniqueInput uniqueId
    )
    {
        var feedback = new FeedbackDbModel
        {
            Id = uniqueId.Id,
            Comment = updateDto.Comment,
            Rating = updateDto.Rating
        };

        if (updateDto.CreatedAt != null)
        {
            feedback.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Event != null)
        {
            feedback.EventId = updateDto.Event;
        }
        if (updateDto.UpdatedAt != null)
        {
            feedback.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            feedback.UserId = updateDto.User;
        }

        return feedback;
    }
}
