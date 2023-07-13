using Application.Infra;
using AutoMapper;
using MediatR;
using Models;
using reality_subscribe_api.Model;

namespace Application.UseCases.Inscricoes.Create
{
    public class CreateInscricaoHandler : IRequestHandler<CreateInscricaoCommand, InscricaoValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IARepository<Subscribe> _inscricaoRepository;
        private readonly IARepository<Models.File> _fileRepository;
        private readonly IARepository<SubscribeFile> _subscribeFileRepository;

        public CreateInscricaoHandler(
            IMapper mapper, IARepository<Subscribe> inscricaoRepository, 
            IARepository<Models.File> fileRepository, 
            IARepository<SubscribeFile> subscribeFileRepository
            )
        {
            _mapper = mapper;   
            _inscricaoRepository = inscricaoRepository;
            _fileRepository = fileRepository;
            _subscribeFileRepository = subscribeFileRepository;
        }

        public async Task<InscricaoValidationResult> Handle(CreateInscricaoCommand request, CancellationToken cancellationToken)
        {
            var inscricao = _mapper.Map<Subscribe>(request);
            _inscricaoRepository.Insert(inscricao);

            if (request.FilesIds != null)
            {
                List<Models.File> files = new();
                
                foreach (var item in request.FilesIds)
                {

                    var imageFile = _fileRepository.GetById(item);
                    if (imageFile.Type.Contains("image")){
                        files.Add(imageFile);
                    }
                    _fileRepository.Delete(item);
                    _fileRepository.Commit();
                }

                _fileRepository.InsertRange(files);

                var file =files
                    .Select(f => new SubscribeFile { SubscribeId = inscricao.Id, FileId = f.Id });
                _subscribeFileRepository.InsertRange(file);
            }

            _inscricaoRepository.Commit();
            _fileRepository.Commit();
            _subscribeFileRepository.Commit();

            return new InscricaoValidationResult
            {
                Id = inscricao.Id,
                Nome = request.Nome,
                Email = request.Email,
            };
        }
    }
}
