using System.Threading;
using System.Threading.Tasks;
using ApplicationService.QueryHandlers;
using Domain._Shared.Repositories;
using Domain.Queries;
using Infrastructure.Queries;
using MediatR;

namespace Infrastructure.Handlers.Queries.User
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, Domain.Entities.User>
    {
        private readonly GetUserByIdHandler _domainHandler;
        public GetUserByIdQueryHandler(IRepositoryManager repositoryManager, GetUserByIdHandler domainHandler)
        {
            _domainHandler = domainHandler;
        }
        
        public async Task<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var domainRequest = new GetUserById(request.UserId);
            var res = await _domainHandler.Handle(domainRequest);
            return res;
        }
    }
}