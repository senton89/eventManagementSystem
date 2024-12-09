using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Extensions;
using EventManagementSystem.Infrastructure;
using EventManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.APIs;

public abstract class FeedbacksServiceBase : IFeedbacksService
{
    protected readonly EventManagementSystemDbContext _context;

    public FeedbacksServiceBase(EventManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Feedback
    /// </summary>
    public async Task<Feedback> CreateFeedback(FeedbackCreateInput createDto)
    {
        var feedback = new FeedbackDbModel
        {
            Comment = createDto.Comment,
            CreatedAt = createDto.CreatedAt,
            Rating = createDto.Rating,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            feedback.Id = createDto.Id;
        }
        if (createDto.Event != null)
        {
            feedback.Event = await _context
                .Events.Where(eventDbModel => createDto.Event.Id == eventDbModel.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.User != null)
        {
            feedback.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FeedbackDbModel>(feedback.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Feedback
    /// </summary>
    public async Task DeleteFeedback(FeedbackWhereUniqueInput uniqueId)
    {
        var feedback = await _context.Feedbacks.FindAsync(uniqueId.Id);
        if (feedback == null)
        {
            throw new NotFoundException();
        }

        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Feedbacks
    /// </summary>
    public async Task<List<Feedback>> Feedbacks(FeedbackFindManyArgs findManyArgs)
    {
        var feedbacks = await _context
            .Feedbacks.Include(x => x.Event)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return feedbacks.ConvertAll(feedback => feedback.ToDto());
    }

    /// <summary>
    /// Meta data about Feedback records
    /// </summary>
    public async Task<MetadataDto> FeedbacksMeta(FeedbackFindManyArgs findManyArgs)
    {
        var count = await _context.Feedbacks.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Feedback
    /// </summary>
    public async Task<Feedback> Feedback(FeedbackWhereUniqueInput uniqueId)
    {
        var feedbacks = await this.Feedbacks(
            new FeedbackFindManyArgs { Where = new FeedbackWhereInput { Id = uniqueId.Id } }
        );
        var feedback = feedbacks.FirstOrDefault();
        if (feedback == null)
        {
            throw new NotFoundException();
        }

        return feedback;
    }

    /// <summary>
    /// Update one Feedback
    /// </summary>
    public async Task UpdateFeedback(
        FeedbackWhereUniqueInput uniqueId,
        FeedbackUpdateInput updateDto
    )
    {
        var feedback = updateDto.ToModel(uniqueId);

        if (updateDto.Event != null)
        {
            feedback.Event = await _context
                .Events.Where(eventDbModel => updateDto.Event == eventDbModel.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.User != null)
        {
            feedback.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(feedback).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Feedbacks.Any(e => e.Id == feedback.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a event record for Feedback
    /// </summary>
    public async Task<Event> GetEvent(FeedbackWhereUniqueInput uniqueId)
    {
        var feedback = await _context
            .Feedbacks.Where(feedback => feedback.Id == uniqueId.Id)
            .Include(feedback => feedback.Event)
            .FirstOrDefaultAsync();
        if (feedback == null)
        {
            throw new NotFoundException();
        }
        return feedback.Event.ToDto();
    }

    /// <summary>
    /// Get a user record for Feedback
    /// </summary>
    public async Task<User> GetUser(FeedbackWhereUniqueInput uniqueId)
    {
        var feedback = await _context
            .Feedbacks.Where(feedback => feedback.Id == uniqueId.Id)
            .Include(feedback => feedback.User)
            .FirstOrDefaultAsync();
        if (feedback == null)
        {
            throw new NotFoundException();
        }
        return feedback.User.ToDto();
    }
}
