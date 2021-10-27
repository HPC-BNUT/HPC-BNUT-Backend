using System;
using Framework.Domain.Events;

namespace Domain.Events
{
    public class UserRegistered : IEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        
    }
}