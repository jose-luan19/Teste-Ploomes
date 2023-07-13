using Application.UseCases.Login;
using Application.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace reality_subscribe_api.Controllers
{
    public class AuthController : RealityControllerBase
    {
        public readonly IMediator _mediator;
        public AuthController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand login)
        {
            var result = await _mediator.Send(login);
            return new ObjectResult(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand register)
        {
            var result = await _mediator.Send(register);
            return new ObjectResult(result);
        }
    }
}
