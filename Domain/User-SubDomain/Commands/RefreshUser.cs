using Framework.Domain.Commands;

namespace Domain.Commands
{
    public class RefreshUser: IDomainCommand
    {
        public static RefreshUser Create(string accessToken, string refreshToken) => new RefreshUser(accessToken, refreshToken);

        private RefreshUser(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get;  }
        public string RefreshToken { get;  }
    }
}