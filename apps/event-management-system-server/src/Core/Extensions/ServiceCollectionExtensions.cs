using EventManagementSystem.APIs;

namespace EventManagementSystem;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IEventsService, EventsService>();
        services.AddScoped<IFeedbacksService, FeedbacksService>();
        services.AddScoped<INotificationsService, NotificationsService>();
        services.AddScoped<IParticipantRegistrationsService, ParticipantRegistrationsService>();
        services.AddScoped<ISessionsService, SessionsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
