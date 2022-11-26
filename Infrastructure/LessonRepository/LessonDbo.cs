using ScheduleSystem.Application.DTOs;

class LessonDbo
{
    public Guid Id { get; set; }
    public int Room { get; set; }
    public string Group { get; set; }
    public string Teacher { get; set; }
    public string Discipline { get; set; }

    public LessonDto ToDto(){
        return new LessonDto{
            Id = Id.ToString(),
            Room = Room,
            Group = Group,
            Teacher = Teacher,
            Discipline = Discipline
        };
    }
}