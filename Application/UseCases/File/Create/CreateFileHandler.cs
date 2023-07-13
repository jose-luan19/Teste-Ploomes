using Application.Infra;
using MediatR;
namespace Application.UseCases.File.Create
{
    public class CreateFileHandler : IRequestHandler<CreateFIleCommand, CreateFileResult>
    {
        private readonly IARepository<Models.File> _fileRepository;

        public CreateFileHandler(IARepository<Models.File> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<CreateFileResult> Handle(CreateFIleCommand request, CancellationToken cancellationToken)
        {
            string fileBase64;
            var formFile = request.File;

            using(var file = new MemoryStream())
            {
                formFile.CopyTo(file);
                var fileBytes = file.ToArray();
                fileBase64 = Convert.ToBase64String(fileBytes);
            }

            var fileAttachment = new Models.File()
            {
                Attachment = fileBase64,
                Name = request.File.FileName,
                Type = request.File.ContentType,
            };

            _fileRepository.Insert(fileAttachment);
            _fileRepository.Commit();

            return new CreateFileResult()
            {
                FileId = fileAttachment.Id,
                Name = fileAttachment.Name,
            };

        }
    }
}
