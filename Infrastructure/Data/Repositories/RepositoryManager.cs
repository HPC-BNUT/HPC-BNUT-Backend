using System.Threading.Tasks;
using Domain._Shared.Repositories;
using Domain.Repositories;
using Infrastructure.Data.DbContext;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly PgSqlDbContext _context;
        private IUserRepository _userRepository;

        public RepositoryManager(PgSqlDbContext context)
        {
            _context = context;
        }

        public IUserRepository User
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}