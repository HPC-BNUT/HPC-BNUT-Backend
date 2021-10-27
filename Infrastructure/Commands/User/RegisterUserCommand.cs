using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Infrastructure.Commands.User
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        [Required]
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
    }
}