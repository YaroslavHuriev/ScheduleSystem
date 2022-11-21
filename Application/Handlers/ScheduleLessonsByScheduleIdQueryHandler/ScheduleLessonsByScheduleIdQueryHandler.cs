using ScheduleSystem.Application.DTOs;
using ScheduleSystem.Infrastructure.LessonTimeRepository;

namespace ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler {
	public class ScheduleLessonsByScheduleIdQueryHandler : IScheduleLessonsByScheduleIdQueryHandler {
		private readonly ILessonTimeRepository _lessonTimeRepository;

		public ScheduleLessonsByScheduleIdQueryHandler(ILessonTimeRepository lessonTimeRepository) {
			_lessonTimeRepository = lessonTimeRepository;
		}

		public async Task<IEnumerable<LessonWithTimeDto>> Handle(string scheduleId) {
			return await _lessonTimeRepository.GetLessonsByScheduleId(scheduleId);
		}
	}
}
