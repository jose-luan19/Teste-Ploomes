using Application.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;
using reality_subscribe_api.Model;

namespace Application.UseCases.Inscricoes.GetAll
{
    public class GetAllInscricaoHandler : IRequestHandler<GetAllInscricaoCommand, GetAllInscricaoCommandResult>
    {
        private readonly IARepository<Subscribe> _repository;

        public GetAllInscricaoHandler(IARepository<Subscribe> repository)
        {
            _repository = repository;
        }

        public async Task<GetAllInscricaoCommandResult> Handle(GetAllInscricaoCommand request, CancellationToken cancellationToken)
        {
            var listWithFiles = _repository.GetAll().Include(f => f.Files).ThenInclude(x => x.File).ToList();


            GetAllInscricaoCommandResult result = new GetAllInscricaoCommandResult
            {
                InscricaoResult = listWithFiles
            };

            return result;


        }
    }
}
