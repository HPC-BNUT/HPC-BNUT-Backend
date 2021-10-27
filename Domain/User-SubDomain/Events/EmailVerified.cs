using System;
using Framework.Domain.Events;

namespace Domain.Events
{
    public class EmailVerified : IEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}