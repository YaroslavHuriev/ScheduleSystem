using ScheduleSystem.Infrastructure.LessonTimeRepository;
using ScheduleSystem.Infrastructure.ScheduleInputRepository;
using ScheduleSystem.Infrastructure.ScheduleRepository;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ILessonTimeRepository, LessonTimeRepository>();
        services.AddTransient<ITeachersRepository, TeachersRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IScheduleRepository, ScheduleRepository>();
        services.AddTransient<ILessonRepository, LessonRepository>();
        services.AddTransient<IScheduleInputRepository, ScheduleInputRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISettingsRepository, SettingsRepository>();

        return services;
    }
}