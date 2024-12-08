using EventManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Infrastructure;

public class EventManagementSystemDbContext : DbContext
{
    public EventManagementSystemDbContext(DbContextOptions<EventManagementSystemDbContext> options)
        : base(options) { }

    public DbSet<EventDbModel> Events { get; set; }

    public DbSet<SessionDbModel> Sessions { get; set; }

    public DbSet<FeedbackDbModel> Feedbacks { get; set; }

    public DbSet<ParticipantRegistrationDbModel> ParticipantRegistrations { get; set; }

    public DbSet<NotificationDbModel> Notifications { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
