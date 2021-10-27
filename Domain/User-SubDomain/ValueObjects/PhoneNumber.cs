using Framework.Domain.ValueObjects;

namespace Domain.ValueObjects
{
    public class PhoneNumber : BaseValueObject<PhoneNumber>
    {
        public string Value { get; }
        public static PhoneNumber FromString(string value) => new PhoneNumber(value);

        private PhoneNumber()
        {
            
        }

        public PhoneNumber(string value)
        {
            // if (string.IsNullOrWhiteSpace(value))
            // {
            //     throw new ArgumentException("PhoneNumber can not be null.", nameof(value));
            // }
            
            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public override bool ObjectIsEqual(PhoneNumber otherObject) => Value == otherObject.Value;

        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    }
}