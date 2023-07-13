using Application.Infra;
using Application.Service;
using AutoMapper;
using Azure;
using MediatR;
using Models;
using reality_subscribe_api.Model;
using System.Text.RegularExpressions;

namespace Application.UseCases.Inscricoes.Create
{
    public class CreateSubscribeHandler : IRequestHandler<CreateSubscribeCommand, SubscribeResult>
    {
        private readonly IMapper _mapper;
        private readonly IARepository<Subscribe> _inscricaoRepository;
        private readonly IARepository<Models.File> _fileRepository;
        private readonly IARepository<SubscribeFile> _subscribeFileRepository;

        public CreateSubscribeHandler(
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

        public async Task<SubscribeResult> Handle(CreateSubscribeCommand request, CancellationToken cancellationToken)
        {
            var inscricao = _mapper.Map<Subscribe>(request);
            RegexEmail.validEmail(inscricao.Email);
            _inscricaoRepository.Insert(inscricao);

            if (request.FilesIds.Count > 0)
            {
                List<Models.File> listFiles = new();
                
                foreach (var fileId in request.FilesIds)
                {
                    var file = _fileRepository.GetById(fileId);
                    if (file == null)
                    {
                        throw new NullReferenceException("Não existe file com o ID: " + fileId.ToString());
                    }
                    listFiles.Add(file);
                }

                var sbuscribeFile = listFiles
                    .Select(f => new SubscribeFile { SubscribeId = inscricao.Id, FileId = f.Id });
                _subscribeFileRepository.InsertRange(sbuscribeFile);
                _subscribeFileRepository.Commit();
            }

            _inscricaoRepository.Commit();

            return new SubscribeResult
            {
                Id = inscricao.Id,
                Nome = request.Nome,
                Email = request.Email,
            };
        }
    }
}
