using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);
        public Task<User> GetUserAsync(Guid userId);
        public Task<bool> WithEmailExistAsync(string email);
        public Task<bool> WithNationalIdExistAsync(string nationalId);
    }
}