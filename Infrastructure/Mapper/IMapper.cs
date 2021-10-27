using System.Threading.Tasks;
using Framework.Domain.Commands;
using Framework.Domain.Queries;

namespace Infrastructure.Mapper
{
    public interface IMapper
    {
        Task<TDestination> MapAsync<TSource, TDestination>(TSource source, TDestination destination);
    }
}