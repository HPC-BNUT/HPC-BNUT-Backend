using System;
using System.Security.Cryptography;
using System.Text;
using Framework.Domain.ValueObjects;
using Framework.Exceptions;

namespace Domain.ValueObjects
{
    public class PasswordHash : BaseValueObject<PasswordHash>
    {
        public string Value { get; }

        public static PasswordHash FromHashedString(string hashedString) => new PasswordHash(hashedString);

        public static PasswordHash FromNotHashedString(string password)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();  
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return new PasswordHash(builder.ToString());
        }

        public PasswordHash(string hashedString)
        {
            if (hashedString.Length < 5)
            {
                throw new BadRequestException("Password is too short must be more than 5 characters.");
            }

            Value = hashedString;
        }

        public override bool ObjectIsEqual(PasswordHash otherObject) => Value == otherObject.Value;
        public override int ObjectGetHashCode() => Value.GetHashCode();
        
        public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
    }
}