using Domain.Commands;
using Infrastructure.Commands.User;

namespace Infrastructure.Mapper
{
    public interface ICommandMapper
    {
        RegisterUser MapRegisterUserCommand(RegisterUserCommand command);
        LoginUser MapLoginUserCommand(LoginUserCommand command);
        RefreshUser MapRefreshUserCommand(RefreshUserCommand command);
    }
}