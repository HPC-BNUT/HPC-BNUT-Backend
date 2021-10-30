using Domain._Shared.ValueObjects;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).HasConversion(x => x.Value, d => Email.FromString(d));
            builder.Property(u => u.FirstName).HasConversion(x => x.Value, d => FirstName.FromString(d));
            builder.Property(u => u.LastName).HasConversion(x => x.Value, d => LastName.FromString(d));
            builder.Property(u => u.NationalId).HasConversion(x => x.Value, d => NationalId.FromString(d));
            builder.Property(u => u.PhoneNumber).HasConversion(x => x.Value, d => PhoneNumber.FromString(d));
            builder.Property(u => u.PasswordHash).HasConversion(x => x.Value, d => PasswordHash.FromHashedString(d));
            builder.Property(u => u.LastLoginDateTime).HasConversion(x => x.Value, d => LastLoginDateTime.FromDateTime(d));
            builder.Property(u => u.CreatedDateTime).HasConversion(x => x.Value, d => CreatedDateTime.FromDateTime(d));
            builder.Property(u => u.RefreshTokenHash).HasConversion(x => x.Value, d => RefreshTokenHash.FromHashedString(d));
            builder.Property(u => u.RefreshTokenExpireTime).HasConversion(x => x.Value, d => RefreshTokenExpireTime.FromDateTime(d));
        }
    }
}