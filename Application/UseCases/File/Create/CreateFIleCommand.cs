using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.File.Create
{
    public class CreateFIleCommand : IRequest<CreateFileResult>
    {
       public IFormFile File { get; set; }
    }
}
