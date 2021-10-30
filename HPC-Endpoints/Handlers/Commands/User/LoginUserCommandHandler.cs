using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationService.CommandHandlers;
using Infrastructure;
using Infrastructure.Commands.User;
using Infrastructure.Mapper;
using MediatR;

namespace HPC_Endpoints.Handlers.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, PairToken>
    {
        private readonly LoginUserHandler _loginUserHandler;
        private readonly IJwtTokenCreator _jwtTokenCreator;
        private readonly ICommandMapper _commandMapper;
        public LoginUserCommandHandler(LoginUserHandler loginUserHandler, ICommandMapper commandMapper, IJwtTokenCreator jwtTokenCreator)
        {
            _loginUserHandler = loginUserHandler;
            _commandMapper = commandMapper;
            _jwtTokenCreator = jwtTokenCreator;
        }
        
        public async Task<PairToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var domainRequest = _commandMapper.MapLoginUserCommand(request);
            var res = await _loginUserHandler.Handle(domainRequest);
            var tokens = GetNewTokens(res);
            return tokens;
        }

        #region privates
        private PairToken GetNewTokens(Domain.Entities.User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            
            var pairTokens = new PairToken()
            {
                AccessToken = _jwtTokenCreator.CreateAccessToken(claims)
            };

            return pairTokens;
        }
        #endregion
    }
}