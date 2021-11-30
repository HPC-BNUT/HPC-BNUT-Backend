using System.ComponentModel.DataAnnotations;
using ApplicationService._Shared.Models;
using ApplicationService._Shared.Services;
using MediatR;

namespace Infrastructure.Commands.User
{
    public class RefreshUserCommand : IRequest<PairToken>
    {
        [Required]
        public string AccessToken { get; set; }
        
        [Required]
        public string RefreshToken { get; set; }

        
    }
}