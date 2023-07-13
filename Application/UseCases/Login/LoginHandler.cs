using Application.Infra;
using Application.Service;
using DevOne.Security.Cryptography.BCrypt;
using MediatR;
using reality_subscribe_api.Model;

namespace Application.UseCases.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IARepository<User> _userRepository;

        public LoginHandler(IARepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                throw new ArgumentException("Campos vazios");
            }
            RegexEmail.validEmail(request.Email);


            var user = _userRepository.Queryable()
                .Where(x => x.Email == request.Email)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentException("Usuario nao existe");
            }

            if (!BCryptHelper.CheckPassword(request.Senha, user.Senha))
            {
                throw new ArgumentException("Senha incorreta");
            }

            return new LoginCommandResult
            {
                hasLogin = true
            };
        }
    }
}
