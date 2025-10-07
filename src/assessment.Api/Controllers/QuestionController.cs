using assessment.Application.Abstractions.Messaging;
using assessment.Application.Dtos.Question;
using assessment.Application.UseCase.Questions.Command.Create;
using assessment.Application.UseCase.Questions.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> QuestionList([FromQuery] GetAllQuestionQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllQuestionQuery, IEnumerable<QuestionResponseDTO>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateQuestionCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
