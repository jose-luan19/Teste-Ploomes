using Application.UseCases.Inscricoes.Confirm;
using Application.UseCases.Inscricoes.Create;
using Application.UseCases.Inscricoes.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace reality_subscribe_api.Controllers
{
    public class SubscribeController : RealityControllerBase
    {
        public readonly IMediator _mediator;
        public SubscribeController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateSubscribeCommand inscricao)
        {
            SubscribeResult result;
            try
            {
                result  = await _mediator.Send(inscricao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPut("Confirm")]
        public async Task<ActionResult> Confirm(ConfirmSubscribeCommand subs)
        {
            try
            {
                await _mediator.Send(subs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<GetAllInscricaoCommandResult>> GetAll([FromQuery] GetAllInscricaoCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
