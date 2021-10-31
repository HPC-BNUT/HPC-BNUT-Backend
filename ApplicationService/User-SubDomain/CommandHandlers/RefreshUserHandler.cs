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
    public class RefreshUserHandler : ICommandHandler<RefreshUser, PairToken>
    {
        private readonly IJwtTokenCreator _jwtTokenCreator;
        private readonly IRepositoryManager _repositoryManager;

        public RefreshUserHandler(IJwtTokenCreator jwtTokenCreator, IRepositoryManager repositoryManager)
        {
            _jwtTokenCreator = jwtTokenCreator;
            _repositoryManager = repositoryManager;
        }

        public async Task<PairToken> Handle(RefreshUser command)
        {
            var principles = _jwtTokenCreator.GetPrincipalFromExpiredToken(command.AccessToken);
            var user = await _repositoryManager.User.GetUserByEmailAsync(Email.FromString(principles.Identity.Name),
                trackChanges: true);

            if (user is null)
            {
                throw new NotFoundException("Credentials are incorrect.");
            }

            user.CheckRefreshTokenHash(RefreshTokenHash.FromNotHashedString(command.RefreshToken));
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