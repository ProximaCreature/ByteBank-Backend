using System.ComponentModel.DataAnnotations;

namespace ByteBank.API.Security.Domain.Models.Commands;

public record UpdateUserCommand(
    [Required] string Username,
    [Required] string Password
);