using System;
using Framework.Domain.ValueObjects;

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
                throw new ArgumentException("FirstName can not be null.", nameof(value));
            }
            
            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public override bool ObjectIsEqual(FirstName otherObject) => Value == otherObject.Value;

        public static implicit operator string(FirstName firstName) => firstName.Value;
    }
}