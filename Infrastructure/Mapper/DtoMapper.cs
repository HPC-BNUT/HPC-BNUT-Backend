using System;
using Domain.Entities;
using Infrastructure.DataTransferObjects;

namespace Infrastructure.Mapper
{
    public class DtoMapper : IDtoMapper
    {
        public UserDto MapToUserDto(User user)
        {
            var dto = new UserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NationalId = user.NationalId
            };

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                dto.PhoneNumber = user.PhoneNumber;
            }
            
            return dto;
        }
    }
}