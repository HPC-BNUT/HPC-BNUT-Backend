using System;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain.ValueObjects
{
    public class Email : BaseValueObject<Email>
    {
        public string Value { get;}
        public static Email FromString(string value) => new Email(value);

        private Email()
        {
            
        }

        public Email(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new BadRequestException("Email can not be null.");
            }
            
            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();

        public override bool ObjectIsEqual(Email otherObject) => Value == otherObject.Value;

        public static implicit operator string(Email email) => email.Value;
    }
}