using System;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain.ValueObjects
{
    public class LastName : BaseValueObject<LastName>
    {
        public string Value { get; private set; }
        public static LastName FromString(string value) => new LastName(value);
        private LastName()
        {

        }
        public LastName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new BadRequestException("LastName can not be null.");
            }
            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public override bool ObjectIsEqual(LastName otherObject) => Value == otherObject.Value;

        public static implicit operator string(LastName lastName) => lastName.Value;
    }
}