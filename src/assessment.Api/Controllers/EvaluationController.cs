using assessment.Application.Abstractions.Messaging;
using assessment.Application.UseCase.Evaluations.Command.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNivel([FromBody] CreateEvaluationCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateEvaluationCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
