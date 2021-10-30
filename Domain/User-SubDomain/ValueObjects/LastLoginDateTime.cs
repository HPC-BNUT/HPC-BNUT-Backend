using System;
using Framework.Domain.ValueObjects;

namespace Domain.ValueObjects
{
    public class LastLoginDateTime : BaseValueObject<LastLoginDateTime>
    {
        public DateTime Value { get; }

        public static LastLoginDateTime FromDateTime(DateTime value) => new LastLoginDateTime(value);
        public static LastLoginDateTime FromUtcNow() => new LastLoginDateTime(DateTime.UtcNow);
        
        private LastLoginDateTime()
        {
            
        }

        public LastLoginDateTime(DateTime value)
        {
            if (value > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Last login date can not be in the future.");
            }
            
            Value = value;
        }
        public override bool ObjectIsEqual(LastLoginDateTime otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator DateTime(LastLoginDateTime lastLoginDateTime) => lastLoginDateTime.Value;
    }
}