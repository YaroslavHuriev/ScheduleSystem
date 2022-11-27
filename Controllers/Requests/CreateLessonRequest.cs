using System.ComponentModel.DataAnnotations;
using ScheduleSystem.Common.Attributes;

public class CreateLessonRequest
{
    [Required, ValidGuid]
    public string TeacherId { get; set; }
    [Required, ValidGuid]
    public string GroupId { get; set; }
    [Required]
    public int Room { get; set; }
    [Required, ValidGuid]
    public string InputDataId { get; set; }
    [Required]
    public string Discipline { get; set; }
}