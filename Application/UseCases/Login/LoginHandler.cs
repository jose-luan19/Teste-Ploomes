using Application.Infra;
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

            var user = _userRepository.Queryable()
                .Where(x => x.Email == request.Email)
                .FirstOrDefault();

            if (user == null)
            {
                return new LoginCommandResult
                {
                    hasLogin = false,
                    Message = "Usuario nao existe",
                };
            }

            if (!BCryptHelper.CheckPassword(request.Senha, user.Senha))
            {
                return new LoginCommandResult
                {
                    hasLogin = false,
                    Message = "Senha incorreta",
                };
            }

            return new LoginCommandResult
            {
                hasLogin = true
            };
        }
    }
}
