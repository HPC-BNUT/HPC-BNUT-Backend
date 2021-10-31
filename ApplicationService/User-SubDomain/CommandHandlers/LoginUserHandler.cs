using System;
using System.Collections.Generic;
using System.Security.Authentication;
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
    public class LoginUserHandler : ICommandHandler<LoginUser, PairToken>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IJwtTokenCreator _jwtTokenCreator;

        public LoginUserHandler(IRepositoryManager repositoryManager, IJwtTokenCreator jwtTokenCreator)
        {
            _repositoryManager = repositoryManager;
            _jwtTokenCreator = jwtTokenCreator;
        }

        public async Task<PairToken> Handle(LoginUser command)
        {
            var user = await _repositoryManager.User.GetUserByEmailAsync(command.Email, trackChanges: true);

            if (user is null)
                throw new NotFoundException("Credentials are incorrect");

            user.CheckPassword(command.PasswordHash);
            var tokens = GetNewTokens(user);

            user.Login(RefreshTokenHash.FromNotHashedString(tokens.RefreshToken),
                RefreshTokenExpireTime.FromDateTime(DateTime.UtcNow.AddDays(_jwtTokenCreator.GetRefreshTokenExTime())));
            
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