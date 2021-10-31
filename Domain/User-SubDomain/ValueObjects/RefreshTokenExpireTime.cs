using System;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain.ValueObjects
{
    public class RefreshTokenExpireTime : BaseValueObject<RefreshTokenExpireTime>
    {
        public DateTime Value { get; }

        public static RefreshTokenExpireTime FromDateTime(DateTime value) => new RefreshTokenExpireTime(value);
        public static RefreshTokenExpireTime FromUtcNow() => new RefreshTokenExpireTime(DateTime.UtcNow);
        
        private RefreshTokenExpireTime()
        {
            
        }

        public RefreshTokenExpireTime(DateTime value)
        {
            if (value < DateTime.UtcNow)
            {
                throw new BadRequestException("Last login date can not be in the past.");
            }
            
            Value = value;
        }
        public override bool ObjectIsEqual(RefreshTokenExpireTime otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator DateTime(RefreshTokenExpireTime refreshTokenExpireTime) => refreshTokenExpireTime.Value;
    }
}