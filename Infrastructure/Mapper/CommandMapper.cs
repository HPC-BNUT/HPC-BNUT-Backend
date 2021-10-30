using Domain.Commands;
using Infrastructure.Commands.User;

namespace Infrastructure.Mapper
{
    public class CommandMapper : ICommandMapper
    {
        public RegisterUser MapRegisterUserCommand(RegisterUserCommand command)
        {
            return RegisterUser.Create(command.FirstName, command.LastName, command.Email, command.NationalId, command.Password);
        }

        public LoginUser MapLoginUserCommand(LoginUserCommand command)
        {
            return LoginUser.Create(command.Email, command.Password);
        }
    }
}