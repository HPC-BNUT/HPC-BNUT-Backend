using System.Threading.Tasks;

namespace Framework.ApplicationService.IQueryHandlers
{
    public interface IQueryHandler<TQuery, TResponse>
    {
        Task<TResponse> Handle(TQuery query);
    }
}