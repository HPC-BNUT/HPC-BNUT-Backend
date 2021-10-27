using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PgSqlDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateUserAsync(User user)
        {
            await CreateAsync(user);
        }

        public async Task<User> GetUserAsync(Guid userId, bool trackChanges)
            =>
                await FindByCondition(c => c.Id.Equals(userId), trackChanges)
                    .SingleOrDefaultAsync();


        public async Task<bool> WithEmailExistAsync(Email email)
            =>
                await FindByCondition(c => c.Email.Equals(email), trackChanges: false).AnyAsync();

        public async Task<bool> WithNationalIdExistAsync(NationalId nationalId)
            =>
                await FindByCondition(c => c.NationalId.Equals(nationalId), trackChanges: false).AnyAsync();
    }
}