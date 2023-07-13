using MediatR;

namespace Application.UseCases.Login
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
