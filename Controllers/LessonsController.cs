using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using ScheduleSystem.Application.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {

        private readonly ICreateLessonUseCase _createLessonUseCase;
        private readonly ILessonsListQueryHandler _lessonsListQuery;
        private readonly IGetLessonByIdQueryHandler _getLessonByIdQuery;
        public LessonsController(ICreateLessonUseCase createLessonUseCase, ILessonsListQueryHandler lessonsListQuery, IGetLessonByIdQueryHandler getLessonByIdQuery)
        {
            _createLessonUseCase = createLessonUseCase;
            _lessonsListQuery = lessonsListQuery;
            _getLessonByIdQuery = getLessonByIdQuery;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery, Required] string inputDataId)
        {
            var result = await _lessonsListQuery.Handle(inputDataId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLessonById(string id)
        {
            var result = await _getLessonByIdQuery.Handle(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([Required] CreateLessonRequest request)
        {
            var id = await _createLessonUseCase.Execute(new CreateLessonUseCaseInput
            {
                Discipline = request.Discipline,
                GroupId = request.GroupId,
                InputDataId = request.InputDataId,
                Room = request.Room,
                TeacherId = request.TeacherId
            });
            return Created("api/lessons", id);
        }
    }
}
