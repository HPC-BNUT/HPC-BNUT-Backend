using System;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain.ValueObjects
{
    public class NationalId : BaseValueObject<NationalId>
    {
        public string Value { get; }
        public static NationalId FromString(string value) => new NationalId(value);

        private NationalId()
        {
            
        }

        public NationalId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BadRequestException("NationalId can not be null.");
            }

            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public override bool ObjectIsEqual(NationalId otherObject) => Value == otherObject.Value;

        public static implicit operator string(NationalId nationalId) => nationalId.Value;
    }
}