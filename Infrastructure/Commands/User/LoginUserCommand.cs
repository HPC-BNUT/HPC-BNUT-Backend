using System;
using System.ComponentModel.DataAnnotations;
using ApplicationService._Shared.Services;
using MediatR;

namespace Infrastructure.Commands.User
{
    public class LoginUserCommand : IRequest<PairToken>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}