using System.Threading.Tasks;

namespace Framework.ApplicationService.CommandHandlers
{
    public interface ICommandHandler<TCommand, TResponse>
    {
        Task<TResponse> Handle(TCommand command);
    }
}