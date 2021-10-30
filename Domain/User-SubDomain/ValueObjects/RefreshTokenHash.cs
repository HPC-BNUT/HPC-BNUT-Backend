using System;
using System.Security.Cryptography;
using System.Text;
using Framework.Domain.ValueObjects;

namespace Domain.ValueObjects
{
    public class RefreshTokenHash : BaseValueObject<RefreshTokenHash>
    {
        public string Value { get; }

        public static RefreshTokenHash FromHashedString(string hashedString) => new RefreshTokenHash(hashedString);

        public static RefreshTokenHash FromNotHashedString(string password)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();  
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return new RefreshTokenHash(builder.ToString());
        }

        public RefreshTokenHash(string hashedString)
        {
            Value = hashedString;
        }

        public override bool ObjectIsEqual(RefreshTokenHash otherObject) => Value == otherObject.Value;
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public static implicit operator string(RefreshTokenHash refreshTokenHash) => refreshTokenHash.Value;
    }
}