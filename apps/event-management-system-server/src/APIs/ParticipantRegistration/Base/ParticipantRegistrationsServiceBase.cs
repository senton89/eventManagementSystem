using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Extensions;
using EventManagementSystem.Infrastructure;
using EventManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.APIs;

public abstract class ParticipantRegistrationsServiceBase : IParticipantRegistrationsService
{
    protected readonly EventManagementSystemDbContext _context;

    public ParticipantRegistrationsServiceBase(EventManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one ParticipantRegistration
    /// </summary>
    public async Task<ParticipantRegistration> CreateParticipantRegistration(
        ParticipantRegistrationCreateInput createDto
    )
    {
        var participantRegistration = new ParticipantRegistrationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            participantRegistration.Id = createDto.Id;
        }
        if (createDto.Event != null)
        {
            participantRegistration.Event = await _context
                .Events.Where(eventDbModel => createDto.Event.Id == eventDbModel.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.User != null)
        {
            participantRegistration.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.ParticipantRegistrations.Add(participantRegistration);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ParticipantRegistrationDbModel>(
            participantRegistration.Id
        );

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one ParticipantRegistration
    /// </summary>
    public async Task DeleteParticipantRegistration(
        ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        var participantRegistration = await _context.ParticipantRegistrations.FindAsync(
            uniqueId.Id
        );
        if (participantRegistration == null)
        {
            throw new NotFoundException();
        }

        _context.ParticipantRegistrations.Remove(participantRegistration);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many ParticipantRegistrations
    /// </summary>
    public async Task<List<ParticipantRegistration>> ParticipantRegistrations(
        ParticipantRegistrationFindManyArgs findManyArgs
    )
    {
        var participantRegistrations = await _context
            .ParticipantRegistrations.Include(x => x.Event)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return participantRegistrations.ConvertAll(participantRegistration =>
            participantRegistration.ToDto()
        );
    }

    /// <summary>
    /// Meta data about ParticipantRegistration records
    /// </summary>
    public async Task<MetadataDto> ParticipantRegistrationsMeta(
        ParticipantRegistrationFindManyArgs findManyArgs
    )
    {
        var count = await _context
            .ParticipantRegistrations.ApplyWhere(findManyArgs.Where)
            .CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one ParticipantRegistration
    /// </summary>
    public async Task<ParticipantRegistration> ParticipantRegistration(
        ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        var participantRegistrations = await this.ParticipantRegistrations(
            new ParticipantRegistrationFindManyArgs
            {
                Where = new ParticipantRegistrationWhereInput { Id = uniqueId.Id }
            }
        );
        var participantRegistration = participantRegistrations.FirstOrDefault();
        if (participantRegistration == null)
        {
            throw new NotFoundException();
        }

        return participantRegistration;
    }

    /// <summary>
    /// Update one ParticipantRegistration
    /// </summary>
    public async Task UpdateParticipantRegistration(
        ParticipantRegistrationWhereUniqueInput uniqueId,
        ParticipantRegistrationUpdateInput updateDto
    )
    {
        var participantRegistration = updateDto.ToModel(uniqueId);

        if (updateDto.Event != null)
        {
            participantRegistration.Event = await _context
                .Events.Where(eventDbModel => updateDto.Event == eventDbModel.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.User != null)
        {
            participantRegistration.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(participantRegistration).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ParticipantRegistrations.Any(e => e.Id == participantRegistration.Id))
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
    /// Get a event record for ParticipantRegistration
    /// </summary>
    public async Task<Event> GetEvent(ParticipantRegistrationWhereUniqueInput uniqueId)
    {
        var participantRegistration = await _context
            .ParticipantRegistrations.Where(participantRegistration =>
                participantRegistration.Id == uniqueId.Id
            )
            .Include(participantRegistration => participantRegistration.Event)
            .FirstOrDefaultAsync();
        if (participantRegistration == null)
        {
            throw new NotFoundException();
        }
        return participantRegistration.Event.ToDto();
    }

    /// <summary>
    /// Get a user record for ParticipantRegistration
    /// </summary>
    public async Task<User> GetUser(ParticipantRegistrationWhereUniqueInput uniqueId)
    {
        var participantRegistration = await _context
            .ParticipantRegistrations.Where(participantRegistration =>
                participantRegistration.Id == uniqueId.Id
            )
            .Include(participantRegistration => participantRegistration.User)
            .FirstOrDefaultAsync();
        if (participantRegistration == null)
        {
            throw new NotFoundException();
        }
        return participantRegistration.User.ToDto();
    }
}
