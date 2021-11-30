using System.ComponentModel.DataAnnotations;
using ApplicationService._Shared.Models;
using ApplicationService._Shared.Services;
using MediatR;

namespace Infrastructure.Commands.User
{
    public class RegisterUserCommand : IRequest<PairToken>
    {
        [Required(ErrorMessage = "this shit is required.")]
        [MaxLength(255)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [MinLength(3)]
        [RegularExpression(@"\d{3}")]
        public string NationalId { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}