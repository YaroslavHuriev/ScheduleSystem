namespace ScheduleSystem.Domain.Entities
{
    /// <summary>
    /// Фітнес функції
    /// </summary>
    static class FitnessFunctions
    {
        public static int GroupWindowPenalty = 10;//штраф за вікно в групи
        public static int TeacherWindowPenalty = 7;//штраф за вікно у викладача
        public static int LateLessonPenalty = 1;//штраф за пізню пару
        public static int TooMuchOccurrencesOfOneDisciplinePerDayPenalty = 3;//штраф за пізню пару

        public static int LatestHour = 3;//максимальний час, коли зручно проводити пари
        public static int MaxOccurrencesOfOneDisciplinePerDayForGroup = 2;//максимальний час, коли зручно проводити пари

        /// <summary>
        /// Штраф за вікна
        /// </summary>
        public static int Windows(Plan plan, SettingsDto settings)
        {
            var res = 0;

            for (byte day = 0; day < plan.DaysPerWeek; day++)
            {
                var groupHasLessons = new HashSet<string>();
                var teacherHasLessons = new HashSet<string>();

                for (byte hour = 0; hour < plan.HoursPerDay; hour++) foreach (var lesson in plan.HourPlans[day, hour].Lessons)
                    {
                        if (groupHasLessons.Contains(lesson.Group) && plan.GroupHasWindowOnThePreviousHour(day, hour, lesson.Group))
                            res += settings.GroupWindowPenalty;
                        if (teacherHasLessons.Contains(lesson.Teacher) && plan.TeacherHasWindowOnThePreviousHour(day, hour, lesson.Teacher))
                            res += settings.TeacherWindowPenalty;

                        groupHasLessons.Add(lesson.Group);
                        teacherHasLessons.Add(lesson.Teacher);
                    }
            }

            return res;
        }

        public static int TooMuchOfOneDisciplinePerDay(Plan plan, SettingsDto settings)
        {
            var res = 0;

            for (byte day = 0; day < plan.DaysPerWeek; day++)
            {
                var lessons = plan.GetLessonsOfDay(day);
                var uniqueGroups = lessons.Select(l => l.Group).Distinct();
                foreach (var uniqueGroup in uniqueGroups)
                {
                    var disciplinesForGroup = lessons
                    .Where(l => l.Group == uniqueGroup)
                    .Select(l => l.Discipline);
                    var uniqueDisciplinesPerGroup = disciplinesForGroup.Distinct();
                    foreach (var discipline in uniqueDisciplinesPerGroup)
                    {
                        if (disciplinesForGroup.Count(d => d == discipline) > settings.MaxOccurrencesOfOneDisciplinePerDayForGroup)
                        {
                            res += settings.TooMuchOccurrencesOfOneDisciplinePerDayPenalty * (disciplinesForGroup.Count(d => d == discipline) - settings.MaxOccurrencesOfOneDisciplinePerDayForGroup);
                        }
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Штраф за пізні пари
        /// </summary>
        public static int LateLesson(Plan plan, SettingsDto settings)
        {
            var res = 0;
            foreach (var pair in plan.GetLessons())
                if (pair.Hour > settings.LatestHour)
                    res += settings.LateLessonPenalty;

            return res;
        }
    }
}
