using ScheduleSystem.Application.DTOs;
using ScheduleSystem.Domain.Entities;
using ScheduleSystem.Infrastructure.LessonTimeRepository;
using ScheduleSystem.Infrastructure.ScheduleInputRepository;
using ScheduleSystem.Infrastructure.ScheduleRepository;

namespace ScheduleSystem.Application.UseCases
{
    public class GenerateScheduleUseCase : IGenerateScheduleUseCase {
		private readonly IScheduleInputRepository _scheduleInputRepository;
		private readonly IScheduleRepository _scheduleRepository;
		private readonly ILessonTimeRepository _lessonTimeRepository;
		private readonly ILessonRepository _lessonRepository;
		private readonly ISettingsRepository _settingsRepository;

        public GenerateScheduleUseCase(IScheduleInputRepository scheduleInputRepository, IScheduleRepository scheduleRepository, ILessonTimeRepository lessonTimeRepository, ILessonRepository lessonRepository, ISettingsRepository settingsRepository)
        {
            _scheduleInputRepository = scheduleInputRepository;
            _scheduleRepository = scheduleRepository;
            _lessonTimeRepository = lessonTimeRepository;
            _lessonRepository = lessonRepository;
            _settingsRepository = settingsRepository;
        }

        public async Task<string> Execute(string inputDataId, string name) {
			var lessonsResult = await _lessonRepository.GetLessonsByInputDataId(inputDataId);
			var lessons = lessonsResult.Select(d => new Lessоn(d.Group, d.Teacher, d.Room, d.Discipline, d.Id)).ToList();
			var uniqueGroups = lessons.Select(g => g.Group).Distinct().ToList();
			var solver = new Solver();//створюємо солвер

			solver.FitnessFunctions.Add(FitnessFunctions.Windows);//штрафуватимемо за вікна
			solver.FitnessFunctions.Add(FitnessFunctions.LateLesson);//штрафуватимемо за пізні пари
			solver.FitnessFunctions.Add(FitnessFunctions.TooMuchOfOneDisciplinePerDay);//штрафуватимемо за пізні пари

			var res = solver.Solve(lessons.ToList(), await _settingsRepository.GetSettings());//знаходимо найкращий розклад
			var schedule = res.HourPlans;
			var lessonsWithTimes = new List<LessonTimeDto>();
			for (byte day = 0; day < res.DaysPerWeek; day++) {
				for (byte hour = 0; hour < res.HoursPerDay; hour++) {
					schedule[day, hour].Lessons.ToList().ForEach(l => {
						lessonsWithTimes.Add(new LessonTimeDto(Guid.NewGuid().ToString(), day, hour, l.Id));
					});
				}
			}
			var scheduleWithId = new ScheduleDto(Guid.NewGuid().ToString(), name, lessonsWithTimes);
			await _scheduleRepository.CreateSchedule(scheduleWithId);
			await _lessonTimeRepository.CreateLessonsWithTimes(scheduleWithId.Id, scheduleWithId.Lessons);
			return scheduleWithId.Id;
		}
	}
}
