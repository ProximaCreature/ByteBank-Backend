using System.ComponentModel.DataAnnotations;

namespace ByteBank.API.Security.Domain.Models.Commands;

public record DeleteUserCommand(
    [Required] int Id
);