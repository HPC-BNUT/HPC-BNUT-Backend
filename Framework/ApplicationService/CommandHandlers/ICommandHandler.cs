using System.Threading.Tasks;

namespace Framework.ApplicationService.CommandHandlers
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command);
    }
}