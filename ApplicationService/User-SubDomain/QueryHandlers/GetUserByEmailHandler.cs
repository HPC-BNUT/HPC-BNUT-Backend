using System.Threading.Tasks;
using Domain._Shared.Repositories;
using Domain.Entities;
using Domain.Queries;
using Framework.ApplicationService.IQueryHandlers;

namespace ApplicationService.QueryHandlers
{
    public class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, User>
    {
        private readonly IRepositoryManager _repositoryManager;
        public GetUserByEmailHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        
        public async Task<User> Handle(GetUserByEmail query)
        {
            var user = await _repositoryManager.User.GetUserByEmailAsync(query.Email, trackChanges: false);
            return user;
        }
    }
}