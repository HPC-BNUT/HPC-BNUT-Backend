using System;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain.ValueObjects
{
    public class FirstName : BaseValueObject<FirstName>
    {

        public string Value { get; }
        public static FirstName FromString(string value) => new FirstName(value);
        private FirstName()
        {

        }
        public FirstName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new BadRequestException("FirstName can not be null.");
            }
            
            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public override bool ObjectIsEqual(FirstName otherObject) => Value == otherObject.Value;

        public static implicit operator string(FirstName firstName) => firstName.Value;
    }
}