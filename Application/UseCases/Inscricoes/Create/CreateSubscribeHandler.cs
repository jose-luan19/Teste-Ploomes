using Application.Infra;
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
            validEmail(inscricao.Email);
            _inscricaoRepository.Insert(inscricao);

            if (request.FilesIds.Count > 0)
            {
                List<Models.File> files = new();
                
                foreach (var item in request.FilesIds)
                {
                    var imageFile = _fileRepository.GetById(item);
                    files.Add(imageFile);
                }

                var file =files
                    .Select(f => new SubscribeFile { SubscribeId = inscricao.Id, FileId = f.Id });
                _subscribeFileRepository.InsertRange(file);
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

        private void validEmail(string email)
        {
            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (!rg.IsMatch(email))
            {
                throw new ArgumentException();
            }
        }
    }
}
