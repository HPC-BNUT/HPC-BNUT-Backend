using System;
using System.Security.Authentication;
using Domain._Shared.ValueObjects;
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
        public CreatedDateTime CreatedDateTime { get; private set; }
        public RefreshTokenHash RefreshTokenHash { get; private set; }
        public LastLoginDateTime LastLoginDateTime { get; private set; }
        public RefreshTokenExpireTime RefreshTokenExpireTime { get; private set; }
       
        public UserRole UserRole { get; private set; }
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
                PasswordHash = passwordHash,
                PhoneNumber = string.Empty,
                CreatedDateTime = CreatedDateTime.FromUtcNow()
            });
        }

        public void CheckPassword(PasswordHash passwordHash)
        {
            if (!PasswordHash.Equals(passwordHash))
                throw new InvalidCredentialException("Credentials  are incorrect.");
        }

        public void CheckRefreshTokenHash(RefreshTokenHash refreshTokenHash)
        {
            if (!RefreshTokenHash.Equals(refreshTokenHash))
                throw new InvalidCredentialException("Credentials  are incorrect.");
        }

        public void Login(RefreshTokenHash refreshTokenHash, RefreshTokenExpireTime refreshTokenExpireTime)
        {
            HandleEvent(new UserLoggedIn()
            {
                UserId = Id,
                LastLoginDateTime = DateTime.UtcNow,
                RefreshTokenHash = refreshTokenHash,
                RefreshTokenExpireTime = refreshTokenExpireTime
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
                    PhoneNumber = PhoneNumber.FromString(e.PhoneNumber);
                    RegistrationState = RegistrationState.WaitingForEmailVerification;
                    UserRole = UserRole.User;
                    NationalId = NationalId.FromString(e.NationalId);
                    PasswordHash = PasswordHash.FromHashedString(e.PasswordHash);
                    RefreshTokenHash = RefreshTokenHash.FromHashedString(e.RefreshTokenHash);
                    break;
                
                case UserLoggedIn e:
                    LastLoginDateTime = LastLoginDateTime.FromDateTime(e.LastLoginDateTime);
                    RefreshTokenHash = RefreshTokenHash.FromHashedString(e.RefreshTokenHash);
                    RefreshTokenExpireTime = RefreshTokenExpireTime.FromDateTime(e.RefreshTokenExpireTime);
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