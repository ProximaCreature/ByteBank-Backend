using System.ComponentModel.DataAnnotations;

namespace ByteBank.API.Wallet.Domain.Models.Commands;

public record CreateWalletCommand(
    [Required] [MinLength(1, ErrorMessage = "Nombre de Cartera cannot be empty")] string NombreCartera,
    [Required] decimal TasaInteres,
    [Required] [MinLength(1, ErrorMessage = "Tipo de Tasa de Interés cannot be empty")] string TipoTasaInteres,
    [Required] [MinLength(1, ErrorMessage = "Periodo de la Tasa cannot be empty")] string PeriodoTasa,
    [Required] [MinLength(1, ErrorMessage = "Periodo de Capitalización cannot be empty")] string PeriodoCapitalizacion,
    [Required] decimal ComisionActivacionPorLetra,
    [Required] decimal Portes,
    [Required] decimal PorcentajeRetencion,
    [Required] decimal GastosAdministracion,
    [Required] decimal PorcentajeSeguroDegravamen,
    [Required] decimal PorcentajeSeguroRiesgo,
    [Required] int PlazoOperacion,
    [Required] DateTime FechaDescuento
    );