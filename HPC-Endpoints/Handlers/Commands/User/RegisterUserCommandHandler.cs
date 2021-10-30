using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationService._Shared.Services;
using ApplicationService.CommandHandlers;
using Infrastructure;
using Infrastructure.Commands.User;
using Infrastructure.Mapper;
using MediatR;

namespace HPC_Endpoints.Handlers.Commands.User
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, PairToken>
    {
        private readonly RegisterUserHandler _registerUserHandler;
        private readonly ICommandMapper _commandMapper;
        public RegisterUserCommandHandler(RegisterUserHandler registerUserHandler, ICommandMapper commandMapper)
        {
            _registerUserHandler = registerUserHandler;
            _commandMapper = commandMapper;
            
        }
        
        public async Task<PairToken> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var domainCommand = _commandMapper.MapRegisterUserCommand(request);
            var res = await _registerUserHandler.Handle(domainCommand);
            return res;
        }
        
    }
}