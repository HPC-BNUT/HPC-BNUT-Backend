using System.Threading.Tasks;
using Domain.Repositories;

namespace Domain._Shared.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        Task SaveAsync();
    }
}