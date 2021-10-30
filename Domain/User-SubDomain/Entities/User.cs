using System;
using System.Security.Authentication;
using Domain.Events;
using Domain.ValueObjects;
using Framework.Domain.Entities;
using Framework.Domain.Events;
using Framework.Domain.Exceptions;

namespace Domain.Entities
{
    public class User : BaseAggregateRoot<Guid>
    {
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public NationalId NationalId { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        
        public LastLoginDateTime LastLoginDateTime { get; private set; }
        public RegistrationState RegistrationState { get; private set; }

        private User()
        {
        }

        public User(FirstName firstName, LastName lastName, Email email, NationalId nationalId, PasswordHash passwordHash)
        {
            HandleEvent(new UserRegistered
            {
                UserId = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                NationalId = nationalId,
                PasswordHash = passwordHash
            });
        }

        public void Login(PasswordHash passwordHash)
        {
            if (!PasswordHash.Equals(passwordHash))
                throw new InvalidCredentialException("Credentials for login are incorrect.");
            
            
            HandleEvent(new UserLoggedIn()
            {
                UserId = Id,
                LastLoginDateTime = DateTime.UtcNow
            });
        }

        public void VerifyEmail()
        {
            HandleEvent(new EmailVerified()
            {
                Email = Email,
                UserId = Id
            });
        }
        
        protected override void SetStateByEvent(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case UserRegistered e:
                    Id = e.UserId;
                    FirstName = FirstName.FromString(e.FirstName);
                    LastName = LastName.FromString(e.LastName);
                    Email = Email.FromString(e.Email);
                    RegistrationState = RegistrationState.WaitingForEmailVerification;
                    NationalId = NationalId.FromString(e.NationalId);
                    PasswordHash = PasswordHash.FromHashedString(e.PasswordHash);
                    break;
                
                case UserLoggedIn e:
                    LastLoginDateTime = LastLoginDateTime.FromDateTime(e.LastLoginDateTime);
                    break;
                
                case EmailVerified e:
                    RegistrationState = RegistrationState.Registered;
                    break;

                default:
                    throw new InvalidOperationException("Can not do what you request.");
            }
        }

        protected override void ValidateInvariants()
        {
            var isValid = Id != Guid.Empty &&
                          (RegistrationState switch
                          {
                              RegistrationState.WaitingForEmailVerification =>
                                  !string.IsNullOrEmpty(Email.Value),
                              RegistrationState.Registered =>
                                  !string.IsNullOrEmpty(Email.Value)
                                  ,
                              _ =>
                                  true
                          });

            if (!isValid)
            {
                throw new InvalidEntityStateException(this,
                    $"User {Id} is in wrong state.");
            }
        }
    }
}