using System;
using System.Threading.Tasks;
using Domain._Shared.Repositories;
using Domain.Commands;
using Domain.Entities;
using Domain.ValueObjects;
using Framework.ApplicationService.CommandHandlers;

namespace ApplicationService.CommandHandlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser, Guid>
    {
        private readonly IRepositoryManager _repositoryManager;
        public RegisterUserHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        
        public async Task<Guid> Handle(RegisterUser command)
        {
            if (await _repositoryManager.User.WithEmailExistAsync(Email.FromString(command.Email)))
            {
                throw new InvalidOperationException($"User already exists with email \"{command.Email}\" .");
            }
            
            if (await _repositoryManager.User.WithNationalIdExistAsync(NationalId.FromString(command.NationalId)))
            {
                throw new InvalidOperationException($"User already exists with nationalId \"{command.NationalId}\" .");
            }

            var user = new User( FirstName.FromString(command.FirstName),
                LastName.FromString(command.LastName),
                Email.FromString(command.Email),
                NationalId.FromString(command.NationalId),
                PasswordHash.FromHashedString(command.PasswordHash));
            
            await _repositoryManager.User.CreateUserAsync(user);
            await _repositoryManager.SaveAsync();

            return user.Id;
        }
    }
}