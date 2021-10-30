using System.Threading.Tasks;
using Domain._Shared.Repositories;
using Domain.Entities;
using Domain.Queries;
using Framework.ApplicationService.IQueryHandlers;

namespace ApplicationService.QueryHandlers
{
    public class GetUserByIdHandler : IQueryHandler<GetUserById, User>
    {
        private readonly IRepositoryManager _repositoryManager;
        public GetUserByIdHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        
        public async Task<User> Handle(GetUserById query)
        {
            var user = await _repositoryManager.User.GetUserByIdAsync(query.UserId, trackChanges: false);
            return user;
        }
    }
}