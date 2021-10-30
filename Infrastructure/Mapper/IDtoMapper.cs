using Domain.Entities;
using Infrastructure.DataTransferObjects;

namespace Infrastructure.Mapper
{
    public interface IDtoMapper
    {
        UserDto MapToUserDto(User user);
    }
}