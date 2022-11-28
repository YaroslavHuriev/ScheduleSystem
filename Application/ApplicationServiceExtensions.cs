using ScheduleSystem.Application.Handlers.ScheduleInputListQueryHandler;
using ScheduleSystem.Application.Handlers.ScheduleInputQueryHandler;
using ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler;
using ScheduleSystem.Application.UseCases;
using ScheduleSystem.Application.UseCases.CreateInputDataUseCase;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IGenerateScheduleUseCase, GenerateScheduleUseCase>();
        services.AddTransient<ICreateInputDataUseCase, CreateInputDataUseCase>();
        services.AddTransient<ICreateLessonUseCase, CreateLessonUseCase>();
        services.AddTransient<ICreateGroupUseCase, CreateGroupUseCase>();
        services.AddTransient<IDeleteInputDataUseCase, DeleteInputDataUseCase>();
        services.AddTransient<IDeleteScheduleByIdUseCase, DeleteScheduleByIdUseCase>();
        services.AddTransient<IDeleteGroupUseCase, DeleteGroupUseCase>();
        services.AddTransient<ICreateTeacherUseCase, CreateTeacherUseCase>();
        services.AddTransient<IDeleteTeacherUseCase, DeleteTeacherUseCase>();
        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        services.AddTransient<IScheduleInputListQueryHandler, ScheduleInputListQueryHandler>();
        services.AddTransient<IScheduleInputQueryHandler, ScheduleInputQueryHandler>();
        services.AddTransient<IScheduleLessonsByScheduleIdQueryHandler, ScheduleLessonsByScheduleIdQueryHandler>();
        services.AddTransient<IScheduleListQueryHandler, ScheduleListQueryHandler>();
        services.AddTransient<IGetTeachersListQueryHandler, GetTeachersListQueryHandler>();
        services.AddTransient<IGetGroupsListQueryHandler, GetGroupsListQueryHandler>();
        services.AddTransient<ILessonsListQueryHandler, LessonsListQueryHandler>();
        services.AddTransient<IGetLessonByIdQueryHandler, GetLessonByIdQueryHandler>();
        return services;
    }
}