using Application.UseCases.Login;
using Application.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wavefront.SDK.CSharp.Common;

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
        public async Task<ActionResult> Login(LoginCommand login)
        {
            LoginCommandResult result;

            try
            {
                result = await _mediator.Send(login);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterCommand register)
        {
            RegisterCommandResult result;

            try
            {
                result = await _mediator.Send(register);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }
    }
}
