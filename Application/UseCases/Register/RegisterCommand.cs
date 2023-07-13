using MediatR;

namespace Application.UseCases.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResult>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
