using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using ScheduleSystem.Application.DTOs;
using ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler;
using ScheduleSystem.Application.UseCases;
using ScheduleSystem.Common.Attributes;
using ScheduleSystem.Controllers.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleSystem.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulesController : ControllerBase {
		private readonly IGenerateScheduleUseCase _generateSchedule;
		private readonly IScheduleLessonsByScheduleIdQueryHandler _scheduleLessonsByScheduleIdQuery;
		private readonly IScheduleListQueryHandler _scheduleListQuery;

        public SchedulesController(IGenerateScheduleUseCase generateSchedule, IScheduleLessonsByScheduleIdQueryHandler scheduleLessonsByScheduleIdQuery, IScheduleListQueryHandler scheduleListQuery)
        {
            _generateSchedule = generateSchedule;
            _scheduleLessonsByScheduleIdQuery = scheduleLessonsByScheduleIdQuery;
            _scheduleListQuery = scheduleListQuery;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
		public async Task<IEnumerable<LessonWithTimeDto>> Get([FromRoute, Required, ValidGuid]string id) {
			return await _scheduleLessonsByScheduleIdQuery.Handle(id);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAsync()
		{
			var result = await _scheduleListQuery.Handle();
			return Ok(result);
		}

		// POST api/<ValuesController>
		[HttpPost]
		public async Task Post([Required] GenerateScheduleRequest request) {
			await _generateSchedule.Execute(request.InputDataId);
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
