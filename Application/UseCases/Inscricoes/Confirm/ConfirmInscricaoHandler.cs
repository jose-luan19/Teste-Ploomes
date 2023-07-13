using Application.Infra;
using MediatR;
using reality_subscribe_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Inscricoes.Confirm
{
    public class ConfirmInscricaoHandler : IRequestHandler<ConfirmInscricaoCommand, ConfirmInscricaoResult>
    {
        private readonly IARepository<Subscribe> _repository;

        public ConfirmInscricaoHandler(IARepository<Subscribe> repository)
        {
            _repository = repository;
        }

        public async Task<ConfirmInscricaoResult> Handle(ConfirmInscricaoCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var resultQuery = _repository.GetById(id);
                resultQuery.Checked = true;
                _repository.Update(resultQuery);
                _repository.Commit();
            }
            return new ConfirmInscricaoResult();

        }
    }
}
