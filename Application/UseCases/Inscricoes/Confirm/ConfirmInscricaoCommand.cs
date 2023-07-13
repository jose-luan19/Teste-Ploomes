using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Inscricoes.Confirm
{
    public class ConfirmInscricaoCommand : IRequest<ConfirmInscricaoResult>
    {
        public List<Guid> Ids { get; set; }
    }
}
