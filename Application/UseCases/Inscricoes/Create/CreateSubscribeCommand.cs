using MediatR;

namespace Application.UseCases.Inscricoes.Create
{
    public class CreateSubscribeCommand : IRequest<SubscribeResult>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Guid> FilesIds { get; set; }
    }
}
