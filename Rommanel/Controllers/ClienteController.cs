using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastrarPessoaCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
