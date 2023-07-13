using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModel;
using System.Net;

namespace reality_subscribe_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RealityControllerBase : ControllerBase
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public RealityControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected ErrorDetails GenerateValidationProblemDetails(List<ValidationFailure> errors, string method = "")
        {
            var errorMessage = errors.FirstOrDefault()?.ErrorMessage;
            return GenerateValidationProblemDetails(errorMessage, method);
        }

        private ErrorDetails GenerateValidationProblemDetails(string errorMessage, string method)
        {
            return new ErrorDetails()
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = errorMessage
            };
        }
        protected ActionResult ValidationError(List<ValidationFailure> errors)
        {
            var errorDetails = GenerateValidationProblemDetails(errors);
            if (errorDetails == null)
            {
                throw new ValidationException(nameof(errorDetails));
            }
            return new BadRequestObjectResult(errorDetails);
        }
    }
}
