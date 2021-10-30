using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);
        public Task<User> GetUserByIdAsync(Guid userId, bool trackChanges);
        public Task<User> GetUserByEmailAsync(Email email, bool trackChanges);
        public Task<bool> WithEmailExistAsync(Email email);
        public Task<bool> WithNationalIdExistAsync(NationalId nationalId);
    }
}