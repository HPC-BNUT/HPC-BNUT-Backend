using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationService.CommandHandlers;
using Infrastructure.Commands.User;
using Infrastructure.Mapper;
using MediatR;

namespace Infrastructure.Handlers.Commands.User
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly RegisterUserHandler _registerUserHandler;
        private readonly ICommandMapper _commandMapper;
        public RegisterUserCommandHandler(RegisterUserHandler registerUserHandler, ICommandMapper commandMapper)
        {
            _registerUserHandler = registerUserHandler;
            _commandMapper = commandMapper;
        }
        
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var domainCommand = _commandMapper.MapRegisterUserCommand(request);
            var res = await _registerUserHandler.Handle(domainCommand);
            return res;
        }
    }
}