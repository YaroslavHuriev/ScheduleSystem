using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Schedule.Application.DTOs;

using ScheduleSystem.Application.Handlers.ScheduleInputListQueryHandler;
using ScheduleSystem.Application.Handlers.ScheduleInputQueryHandler;

namespace ScheduleSystem.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ScheduleInputDataController : ControllerBase {
		private readonly ILogger<ScheduleInputDataController> _logger;
		private readonly IScheduleInputListQueryHandler _scheduleInputListQuery;
		private readonly IScheduleInputQueryHandler _scheduleInputQuery;

		public ScheduleInputDataController(ILogger<ScheduleInputDataController> logger, IScheduleInputListQueryHandler scheduleInputListQuery, IScheduleInputQueryHandler scheduleInputQuery) {
			_logger = logger;
			_scheduleInputListQuery = scheduleInputListQuery;
			_scheduleInputQuery = scheduleInputQuery;
		}

		[HttpGet]
		public async Task<IEnumerable<ScheduleInputDto>> Get() {
			return await _scheduleInputListQuery.Handle();
		}

		[HttpGet("{id}")]
		public async Task<ScheduleInputDto> Get([Required] string id) {
			return await _scheduleInputQuery.Handle(id);
		}
	}
}
