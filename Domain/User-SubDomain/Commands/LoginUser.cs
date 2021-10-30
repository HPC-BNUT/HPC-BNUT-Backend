using System.Dynamic;
using Domain.ValueObjects;
using Framework.Domain.Commands;

namespace Domain.Commands
{
    public class LoginUser : IDomainCommand
    {
        public static LoginUser Create(string email, string password) => new LoginUser(email, password);

        private LoginUser(string email, string password)
        {
            Email = Email.FromString(email);
            PasswordHash = PasswordHash.FromNotHashedString(password);
        }

        public Email Email { get; set; }
        public PasswordHash PasswordHash { get; set; }
    }
}