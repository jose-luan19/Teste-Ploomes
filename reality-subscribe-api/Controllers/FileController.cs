using Application.UseCases.File.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace reality_subscribe_api.Controllers
{
    public class FileController : RealityControllerBase
    {
        public readonly IMediator _mediator;

        public FileController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(IFormFile file)
        {
            var result = await _mediator.Send(new CreateFIleCommand()
            {
                File = file
            });

            return Ok(result);
        }
    }
}
