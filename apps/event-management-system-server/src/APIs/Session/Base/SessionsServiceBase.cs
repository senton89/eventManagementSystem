using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Extensions;
using EventManagementSystem.Infrastructure;
using EventManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.APIs;

public abstract class SessionsServiceBase : ISessionsService
{
    protected readonly EventManagementSystemDbContext _context;

    public SessionsServiceBase(EventManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Session
    /// </summary>
    public async Task<Session> CreateSession(SessionCreateInput createDto)
    {
        var session = new SessionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            EndTime = createDto.EndTime,
            Location = createDto.Location,
            StartTime = createDto.StartTime,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            session.Id = createDto.Id;
        }
        if (createDto.Event != null)
        {
            session.Event = await _context
                .Events.Where(eventDbModel => createDto.Event.Id == eventDbModel.Id)
                .FirstOrDefaultAsync();
        }

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SessionDbModel>(session.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Session
    /// </summary>
    public async Task DeleteSession(SessionWhereUniqueInput uniqueId)
    {
        var session = await _context.Sessions.FindAsync(uniqueId.Id);
        if (session == null)
        {
            throw new NotFoundException();
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Sessions
    /// </summary>
    public async Task<List<Session>> Sessions(SessionFindManyArgs findManyArgs)
    {
        var sessions = await _context
            .Sessions.Include(x => x.Event)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return sessions.ConvertAll(session => session.ToDto());
    }

    /// <summary>
    /// Meta data about Session records
    /// </summary>
    public async Task<MetadataDto> SessionsMeta(SessionFindManyArgs findManyArgs)
    {
        var count = await _context.Sessions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Session
    /// </summary>
    public async Task<Session> Session(SessionWhereUniqueInput uniqueId)
    {
        var sessions = await this.Sessions(
            new SessionFindManyArgs { Where = new SessionWhereInput { Id = uniqueId.Id } }
        );
        var session = sessions.FirstOrDefault();
        if (session == null)
        {
            throw new NotFoundException();
        }

        return session;
    }

    /// <summary>
    /// Update one Session
    /// </summary>
    public async Task UpdateSession(SessionWhereUniqueInput uniqueId, SessionUpdateInput updateDto)
    {
        var session = updateDto.ToModel(uniqueId);

        if (updateDto.Event != null)
        {
            session.Event = await _context
                .Events.Where(eventDbModel => updateDto.Event == eventDbModel.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(session).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Sessions.Any(e => e.Id == session.Id))
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
    /// Get a event record for Session
    /// </summary>
    public async Task<Event> GetEvent(SessionWhereUniqueInput uniqueId)
    {
        var session = await _context
            .Sessions.Where(session => session.Id == uniqueId.Id)
            .Include(session => session.Event)
            .FirstOrDefaultAsync();
        if (session == null)
        {
            throw new NotFoundException();
        }
        return session.Event.ToDto();
    }
}
