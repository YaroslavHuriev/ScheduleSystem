using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.LessonTimeRepository {
	public interface ILessonTimeRepository {
		Task CreateLessonsWithTimes(string scheduleId, IEnumerable<LessonTimeDto> lessons);
		Task<IEnumerable<LessonWithTimeDto>> GetLessonsByScheduleId(string scheduleId);
	}
}