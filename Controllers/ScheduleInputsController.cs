using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Schedule.Application.DTOs;

using ScheduleSystem.Application.Handlers.ScheduleInputListQueryHandler;
using ScheduleSystem.Application.Handlers.ScheduleInputQueryHandler;
using ScheduleSystem.Application.UseCases.CreateInputDataUseCase;
using ScheduleSystem.Controllers.Requests;

namespace ScheduleSystem.Controllers {
	[Authorize(Roles = "Admin")]
	[ApiController]
	[Route("api/schedule/inputs")]
	public class ScheduleInputsController : ControllerBase {
		private readonly ILogger<ScheduleInputsController> _logger;
		private readonly IScheduleInputListQueryHandler _scheduleInputListQuery;
		private readonly IScheduleInputQueryHandler _scheduleInputQuery;
		private readonly ICreateInputDataUseCase _createInputData;
		private readonly IDeleteInputDataUseCase _deleteInputData;

        public ScheduleInputsController(ILogger<ScheduleInputsController> logger, IScheduleInputListQueryHandler scheduleInputListQuery, IScheduleInputQueryHandler scheduleInputQuery, ICreateInputDataUseCase createInputData, IDeleteInputDataUseCase deleteInputData)
        {
            _logger = logger;
            _scheduleInputListQuery = scheduleInputListQuery;
            _scheduleInputQuery = scheduleInputQuery;
            _createInputData = createInputData;
            _deleteInputData = deleteInputData;
        }

        [HttpGet]
		public async Task<IEnumerable<ScheduleInputDto>> Get() {
			return await _scheduleInputListQuery.Handle();
		}

		[HttpGet("{id}")]
		public async Task<ScheduleInputDto> Get([Required] string id) {
			return await _scheduleInputQuery.Handle(id);
		}

		[HttpPost]
		public async Task<IActionResult> CreateInput([Required] CreateScheduleInputRequest request) {
			var id = await _createInputData.Execute(new CreateInputDataUseCaseInput(request.Name));
			return new CreatedResult("scheduleInput", new { Id = id });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteInput([Required] string id) {
			await _deleteInputData.Execute(id);
			return new OkObjectResult(id);
		}
	}
}
