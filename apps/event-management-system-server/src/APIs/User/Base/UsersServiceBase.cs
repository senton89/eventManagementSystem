using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Extensions;
using EventManagementSystem.Infrastructure;
using EventManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.APIs;

public abstract class UsersServiceBase : IUsersService
{
    protected readonly EventManagementSystemDbContext _context;

    public UsersServiceBase(EventManagementSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    public async Task<User> CreateUser(UserCreateInput createDto)
    {
        var user = new UserDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Password = createDto.Password,
            RecoveryToken = createDto.RecoveryToken,
            Role = createDto.Role,
            Roles = createDto.Roles,
            UpdatedAt = createDto.UpdatedAt,
            Username = createDto.Username
        };

        if (createDto.Id != null)
        {
            user.Id = createDto.Id;
        }
        if (createDto.Feedbacks != null)
        {
            user.Feedbacks = await _context
                .Feedbacks.Where(feedback =>
                    createDto.Feedbacks.Select(t => t.Id).Contains(feedback.Id)
                )
                .ToListAsync();
        }

        if (createDto.Notifications != null)
        {
            user.Notifications = await _context
                .Notifications.Where(notification =>
                    createDto.Notifications.Select(t => t.Id).Contains(notification.Id)
                )
                .ToListAsync();
        }

        if (createDto.ParticipantRegistrations != null)
        {
            user.ParticipantRegistrations = await _context
                .ParticipantRegistrations.Where(participantRegistration =>
                    createDto
                        .ParticipantRegistrations.Select(t => t.Id)
                        .Contains(participantRegistration.Id)
                )
                .ToListAsync();
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserDbModel>(user.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    public async Task DeleteUser(UserWhereUniqueInput uniqueId)
    {
        var user = await _context.Users.FindAsync(uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    public async Task<List<User>> Users(UserFindManyArgs findManyArgs)
    {
        var users = await _context
            .Users.Include(x => x.Feedbacks)
            .Include(x => x.ParticipantRegistrations)
            .Include(x => x.Notifications)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return users.ConvertAll(user => user.ToDto());
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public async Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs)
    {
        var count = await _context.Users.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one User
    /// </summary>
    public async Task<User> User(UserWhereUniqueInput uniqueId)
    {
        var users = await this.Users(
            new UserFindManyArgs { Where = new UserWhereInput { Id = uniqueId.Id } }
        );
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    /// <summary>
    /// Update one User
    /// </summary>
    public async Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto)
    {
        var user = updateDto.ToModel(uniqueId);

        if (updateDto.Feedbacks != null)
        {
            user.Feedbacks = await _context
                .Feedbacks.Where(feedback =>
                    updateDto.Feedbacks.Select(t => t).Contains(feedback.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Notifications != null)
        {
            user.Notifications = await _context
                .Notifications.Where(notification =>
                    updateDto.Notifications.Select(t => t).Contains(notification.Id)
                )
                .ToListAsync();
        }

        if (updateDto.ParticipantRegistrations != null)
        {
            user.ParticipantRegistrations = await _context
                .ParticipantRegistrations.Where(participantRegistration =>
                    updateDto
                        .ParticipantRegistrations.Select(t => t)
                        .Contains(participantRegistration.Id)
                )
                .ToListAsync();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Id == user.Id))
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
    /// Connect multiple Feedbacks records to User
    /// </summary>
    public async Task ConnectFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Feedbacks.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Feedbacks);

        foreach (var child in childrenToConnect)
        {
            parent.Feedbacks.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Feedbacks records from User
    /// </summary>
    public async Task DisconnectFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Feedbacks.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Feedbacks?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Feedbacks records for User
    /// </summary>
    public async Task<List<Feedback>> FindFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackFindManyArgs userFindManyArgs
    )
    {
        var feedbacks = await _context
            .Feedbacks.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return feedbacks.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Feedbacks records for User
    /// </summary>
    public async Task UpdateFeedbacks(
        UserWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Feedbacks.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.Feedbacks = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Notifications records to User
    /// </summary>
    public async Task ConnectNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Notifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Notifications.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Notifications);

        foreach (var child in childrenToConnect)
        {
            parent.Notifications.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Notifications records from User
    /// </summary>
    public async Task DisconnectNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Notifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Notifications.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Notifications?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Notifications records for User
    /// </summary>
    public async Task<List<Notification>> FindNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationFindManyArgs userFindManyArgs
    )
    {
        var notifications = await _context
            .Notifications.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return notifications.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Notifications records for User
    /// </summary>
    public async Task UpdateNotifications(
        UserWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Notifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Notifications.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.Notifications = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple ParticipantRegistrations records to User
    /// </summary>
    public async Task ConnectParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.ParticipantRegistrations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ParticipantRegistrations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.ParticipantRegistrations);

        foreach (var child in childrenToConnect)
        {
            parent.ParticipantRegistrations.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple ParticipantRegistrations records from User
    /// </summary>
    public async Task DisconnectParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.ParticipantRegistrations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ParticipantRegistrations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.ParticipantRegistrations?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple ParticipantRegistrations records for User
    /// </summary>
    public async Task<List<ParticipantRegistration>> FindParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationFindManyArgs userFindManyArgs
    )
    {
        var participantRegistrations = await _context
            .ParticipantRegistrations.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return participantRegistrations.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple ParticipantRegistrations records for User
    /// </summary>
    public async Task UpdateParticipantRegistrations(
        UserWhereUniqueInput uniqueId,
        ParticipantRegistrationWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.ParticipantRegistrations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ParticipantRegistrations.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.ParticipantRegistrations = children;
        await _context.SaveChangesAsync();
    }
}
