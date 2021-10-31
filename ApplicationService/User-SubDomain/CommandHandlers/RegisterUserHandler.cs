using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationService._Shared.Services;
using Domain._Shared.Repositories;
using Domain.Commands;
using Domain.Entities;
using Domain.ValueObjects;
using Framework.ApplicationService.CommandHandlers;
using Framework.Exceptions;

namespace ApplicationService.CommandHandlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser, PairToken>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IJwtTokenCreator _jwtTokenCreator;
        public RegisterUserHandler(IRepositoryManager repositoryManager, IJwtTokenCreator jwtTokenCreator)
        {
            _repositoryManager = repositoryManager;
            _jwtTokenCreator = jwtTokenCreator;
        }
        
        public async Task<PairToken> Handle(RegisterUser command)
        {
            if (await _repositoryManager.User.WithEmailExistAsync(Email.FromString(command.Email)))
            {
                throw new BadRequestException($"User already exists with email: {command.Email.Value}.");
            }
            
            if (await _repositoryManager.User.WithNationalIdExistAsync(NationalId.FromString(command.NationalId)))
            {
                throw new BadRequestException($"User already exists with nationalId: {command.NationalId.Value} .");
            }

            var user = new User( FirstName.FromString(command.FirstName),
                LastName.FromString(command.LastName),
                Email.FromString(command.Email),
                NationalId.FromString(command.NationalId),
                PasswordHash.FromHashedString(command.PasswordHash));
            
            var tokens = GetNewTokens(user);
            user.Login(RefreshTokenHash.FromNotHashedString(tokens.RefreshToken),
                RefreshTokenExpireTime.FromDateTime(DateTime.UtcNow.AddDays(_jwtTokenCreator.GetRefreshTokenExTime())));
            
            await _repositoryManager.User.CreateUserAsync(user);
            await _repositoryManager.SaveAsync();
            
            return tokens;
        }
        
        #region privates
        private PairToken GetNewTokens(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            
            var pairTokens = new PairToken()
            {
                AccessToken = _jwtTokenCreator.CreateAccessToken(claims),
                RefreshToken = _jwtTokenCreator.CreateRefreshToken()
            };

            return pairTokens;
        }
        
        #endregion
    }
}