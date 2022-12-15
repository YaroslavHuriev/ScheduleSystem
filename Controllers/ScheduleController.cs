using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScheduleSystem.Application.DTOs;
using ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler;
using ScheduleSystem.Application.UseCases;
using ScheduleSystem.Common.Attributes;
using ScheduleSystem.Controllers.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IGenerateScheduleUseCase _generateSchedule;
        private readonly IDeleteScheduleByIdUseCase _deleteSchedule;
        private readonly IScheduleLessonsByScheduleIdQueryHandler _scheduleLessonsByScheduleIdQuery;
        private readonly IScheduleListQueryHandler _scheduleListQuery;

        public SchedulesController(IGenerateScheduleUseCase generateSchedule, IScheduleLessonsByScheduleIdQueryHandler scheduleLessonsByScheduleIdQuery, IScheduleListQueryHandler scheduleListQuery, IDeleteScheduleByIdUseCase deleteSchedule)
        {
            _generateSchedule = generateSchedule;
            _scheduleLessonsByScheduleIdQuery = scheduleLessonsByScheduleIdQuery;
            _scheduleListQuery = scheduleListQuery;
            _deleteSchedule = deleteSchedule;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}/lessons")]
        public async Task<IEnumerable<LessonWithTimeDto>> Get([FromRoute, Required, ValidGuid] string id)
        {
            return await _scheduleLessonsByScheduleIdQuery.Handle(id);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAsync()
        {
            var result = await _scheduleListQuery.Handle();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([Required] GenerateScheduleRequest request)
        {
            var id = await _generateSchedule.Execute(request.InputDataId);
            return Created("api/schedules", id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute, Required] string id)
        {
            await _deleteSchedule.Execute(id);
            return Ok();
        }
    }
}
