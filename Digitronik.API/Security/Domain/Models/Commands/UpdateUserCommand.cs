using System.ComponentModel.DataAnnotations;

namespace Digitronik.API.Security.Domain.Models.Commands;

public record UpdateUserCommand(
    [Required] string Username,
    [Required] string Password
);