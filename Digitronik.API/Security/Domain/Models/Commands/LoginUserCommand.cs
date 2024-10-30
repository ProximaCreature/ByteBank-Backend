using System.ComponentModel.DataAnnotations;

namespace Digitronik.API.Security.Domain.Models.Commands;

public record LoginUserCommand(
    [Required] [MinLength(1, ErrorMessage = "CompanyName cannot be empty")] string Username,
    [Required] [MinLength(1, ErrorMessage = "CompanyName cannot be empty")] string Password
);