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
    public class ConfirmSubscribeHandler : IRequestHandler<ConfirmSubscribeCommand, ConfirmSubscribeResult>
    {
        private readonly IARepository<Subscribe> _repository;

        public ConfirmSubscribeHandler(IARepository<Subscribe> repository)
        {
            _repository = repository;
        }

        public async Task<ConfirmSubscribeResult> Handle(ConfirmSubscribeCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var resultQuery = _repository.GetById(id);
                resultQuery.Checked = true;
                _repository.Update(resultQuery);
                _repository.Commit();
            }
            return new ConfirmSubscribeResult();

        }
    }
}
