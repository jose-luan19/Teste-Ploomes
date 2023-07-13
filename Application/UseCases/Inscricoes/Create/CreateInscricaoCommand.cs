using MediatR;

namespace Application.UseCases.Inscricoes.Create
{
    public class CreateInscricaoCommand : IRequest<InscricaoValidationResult>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Guid> FilesIds { get; set; }
    }
}
