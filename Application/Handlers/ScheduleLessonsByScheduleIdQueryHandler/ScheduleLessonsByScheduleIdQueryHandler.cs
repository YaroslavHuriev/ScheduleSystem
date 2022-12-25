using ScheduleSystem.Application.DTOs;
using ScheduleSystem.Infrastructure.LessonTimeRepository;
using ScheduleSystem.Infrastructure.ScheduleRepository;

namespace ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler {
	public class ScheduleLessonsByScheduleIdQueryHandler : IScheduleLessonsByScheduleIdQueryHandler {
		private readonly ILessonTimeRepository _lessonTimeRepository;
		private readonly IScheduleRepository _scheduleRepository;

        public ScheduleLessonsByScheduleIdQueryHandler(ILessonTimeRepository lessonTimeRepository, IScheduleRepository scheduleRepository)
        {
            _lessonTimeRepository = lessonTimeRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ScheduleLessonsWithIsCurrentDto> Handle(string scheduleId) {
			var lessons = await _lessonTimeRepository.GetLessonsByScheduleId(scheduleId);
			var schedule = await _scheduleRepository.GetScheduleById(scheduleId);
			return new ScheduleLessonsWithIsCurrentDto{
				Lessons = lessons,
				IsCurrent = schedule.IsCurrent
			};
		}
	}
}
