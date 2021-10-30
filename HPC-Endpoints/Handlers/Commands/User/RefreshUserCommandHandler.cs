using System.Threading;
using System.Threading.Tasks;
using ApplicationService._Shared.Services;
using ApplicationService.CommandHandlers;
using Infrastructure.Commands.User;
using Infrastructure.Mapper;
using MediatR;

namespace HPC_Endpoints.Handlers.Commands.User
{
    public class RefreshUserCommandHandler : IRequestHandler<RefreshUserCommand, PairToken>
    {
        private readonly RefreshUserHandler _refreshUserHandler;
        private readonly ICommandMapper _commandMapper;
        
        public RefreshUserCommandHandler(RefreshUserHandler refreshUserHandler, ICommandMapper commandMapper)
        {
            _refreshUserHandler = refreshUserHandler;
            _commandMapper = commandMapper;
        }
        
        public async Task<PairToken> Handle(RefreshUserCommand request, CancellationToken cancellationToken)
        {
            var domainRequest = _commandMapper.MapRefreshUserCommand(request);
            var res = await _refreshUserHandler.Handle(domainRequest);
            return res;
        }
    }
}