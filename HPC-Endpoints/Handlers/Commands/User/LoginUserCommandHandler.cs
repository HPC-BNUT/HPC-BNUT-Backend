using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationService._Shared.Models;
using ApplicationService._Shared.Services;
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
        private readonly ICommandMapper _commandMapper;
        public LoginUserCommandHandler(LoginUserHandler loginUserHandler, ICommandMapper commandMapper)
        {
            _loginUserHandler = loginUserHandler;
            _commandMapper = commandMapper;
         
        }
        
        public async Task<PairToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var domainRequest = _commandMapper.MapLoginUserCommand(request);
            var res = await _loginUserHandler.Handle(domainRequest);
            return res;
        }
    }
}