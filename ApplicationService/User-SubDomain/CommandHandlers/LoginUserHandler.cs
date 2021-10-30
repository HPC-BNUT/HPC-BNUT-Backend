using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Domain._Shared.Repositories;
using Domain.Commands;
using Domain.Entities;
using Framework.ApplicationService.CommandHandlers;

namespace ApplicationService.CommandHandlers
{
    public class LoginUserHandler : ICommandHandler<LoginUser, User>
    {
        private readonly IRepositoryManager _repositoryManager;
        
        public LoginUserHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        
        public async Task<User> Handle(LoginUser command)
        {
            var user = await _repositoryManager.User.GetUserByEmailAsync(command.Email, trackChanges: true);

            if (user is null)
                throw new InvalidCredentialException("Credentials are incorrect");
            
            user.Login(command.PasswordHash);
            await _repositoryManager.SaveAsync();
            return user;
        }
    }
}