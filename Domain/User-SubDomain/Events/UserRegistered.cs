using System;
using Domain.Entities;
using Framework.Domain.Events;

namespace Domain.Events
{
    public class UserRegistered : IDomainEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string RefreshTokenHash { get; set; }
    }
}