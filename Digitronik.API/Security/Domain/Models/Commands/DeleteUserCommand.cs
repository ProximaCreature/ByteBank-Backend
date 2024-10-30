using System.ComponentModel.DataAnnotations;

namespace Digitronik.API.Security.Domain.Models.Commands;

public record DeleteUserCommand(
    [Required] int Id
);