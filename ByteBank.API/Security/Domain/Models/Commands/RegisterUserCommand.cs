using System.ComponentModel.DataAnnotations;

namespace ByteBank.API.Security.Domain.Models.Commands;

public record RegisterUserCommand(
    [Required] [MinLength(1, ErrorMessage = "Username cannot be empty")] string Username,
    [Required] [MinLength(1, ErrorMessage = "Password cannot be empty")] string Password,
    [Required] [EmailAddress] [MinLength(1, ErrorMessage = "Email cannot be empty")] string Email,
    [Required] [MinLength(1, ErrorMessage = "FirstName cannot be empty")] string FirstName,
    [Required] [MinLength(1, ErrorMessage = "LastName cannot be empty")] string LastName
);