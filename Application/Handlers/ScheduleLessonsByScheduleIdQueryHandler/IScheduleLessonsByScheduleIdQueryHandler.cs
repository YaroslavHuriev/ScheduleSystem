using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler {
	public interface IScheduleLessonsByScheduleIdQueryHandler {
		Task<IEnumerable<LessonWithTimeDto>> Handle(string scheduleId);
	}
}