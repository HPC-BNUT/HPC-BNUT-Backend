using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;
using Framework.Domain.Commands;

namespace Domain.Commands
{
    public class RegisterUser : IDomainCommand
    {
        public static RegisterUser Create(string firstName, string lastName, string email, string nationalId, string password)
            =>
                new(firstName, lastName, email, nationalId, password);

        private RegisterUser(string firstName, string lastName, string email, string nationalId, string password)
        {
            FirstName = FirstName.FromString(firstName);
            LastName = LastName.FromString(lastName);
            Email = Email.FromString(email);
            NationalId = NationalId.FromString(nationalId);
            PasswordHash = PasswordHash.FromNotHashedString(password);
        }
        public FirstName FirstName { get;  }
        
        public LastName LastName { get;  }
        
        public Email Email { get;  }
        
        public NationalId NationalId { get;  }
        public PasswordHash PasswordHash { get; set; }
    }
}