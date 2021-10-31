using System;
using Domain.ValueObjects;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain._Shared.ValueObjects
{
    public class CreatedDateTime : BaseValueObject<CreatedDateTime>
    {
        public DateTime Value { get; }

        public static CreatedDateTime FromDateTime(DateTime value) => new CreatedDateTime(value);
        public static CreatedDateTime FromUtcNow() => new CreatedDateTime(DateTime.UtcNow);
        
        private CreatedDateTime()
        {
            
        }

        public CreatedDateTime(DateTime value)
        {
            if (value > DateTime.UtcNow)
            {
                throw new BadRequestException("create date can not be in the future.");
            }
            
            Value = value;
        }
        public override bool ObjectIsEqual(CreatedDateTime otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator DateTime(CreatedDateTime createdDateTime) => createdDateTime.Value;
    }
}