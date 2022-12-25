using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler {
	public interface IScheduleLessonsByScheduleIdQueryHandler {
		Task<ScheduleLessonsWithIsCurrentDto> Handle(string scheduleId);
	}
}