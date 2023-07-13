using Application.Infra;
using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using MediatR;
using reality_subscribe_api.Model;

namespace Application.UseCases.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
    {
        private readonly IARepository<User> _repository;
        private readonly IMapper _mapper;

        public RegisterHandler(IARepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return new RegisterCommandResult
                {
                    hasLogin = false,
                    Message = "Campos vazios ou incorretos"
                };
            }
            var user = _repository.Queryable()
                .Where(x => x.Email == request.Email)
                .FirstOrDefault();

            if(user != null) 
            {
                return new RegisterCommandResult
                {
                    hasLogin = false,
                    Message = "Usuario já existe no sistema"
                };
            }

            string salt = BCryptHelper.GenerateSalt();

            request.Senha = BCryptHelper.HashPassword(request.Senha, salt);

            var registro = _mapper.Map<User>(request);

            _repository.Insert(registro);
            _repository.Commit();

            return new RegisterCommandResult
            {
                hasLogin = true
            };

        }
    }
}
