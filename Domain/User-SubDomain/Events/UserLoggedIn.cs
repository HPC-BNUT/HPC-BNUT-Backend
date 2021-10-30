using System;
using Framework.Domain.Events;

namespace Domain.Events
{
    public class UserLoggedIn : IDomainEvent
    {
        public Guid UserId { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}