using assessment.Application.Abstractions.Messaging;
using assessment.Application.Dtos.Dashboard;
using assessment.Application.Dtos.Professor;
using assessment.Application.UseCase.Professors.Command.Create;
using assessment.Application.UseCase.Professors.Queries.Dashboard;
using assessment.Application.UseCase.Professors.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> ProfessorList([FromQuery] GetAllProfessorQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllProfessorQuery, IEnumerable<ProfessorResponseDTO>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Dashboard/{professorId:int}")]
        public async Task<IActionResult> ProfessorDashboard(int professorId)
        {
            var query = new DashboardProfessorQuery { ProfessorId = professorId };

            var response = await _dispatcher
                .Dispatch<DashboardProfessorQuery, ProfessorDashboardDTO>(query, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProfessor([FromBody] CreateProfessorCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateProfessorCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
