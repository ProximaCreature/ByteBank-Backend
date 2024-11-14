using System.ComponentModel.DataAnnotations;

namespace ByteBank.API.Wallet.Domain.Models.Commands;

public record CreateWalletCommand(
    [Required] [MinLength(1, ErrorMessage = "Nombre de Cartera cannot be empty")] string NombreCartera,
    [Required] double TasaInteres,
    [Required] [MinLength(1, ErrorMessage = "Tipo de Tasa de Interés cannot be empty")] string TipoTasaInteres,
    [Required] [MinLength(1, ErrorMessage = "Periodo de la Tasa cannot be empty")] string PeriodoTasa,
    [Required] [MinLength(1, ErrorMessage = "Periodo de Capitalización cannot be empty")] string PeriodoCapitalizacion,
    [Required] double ComisionActivacionPorLetra,
    [Required] double Portes,
    [Required] double PorcentajeRetencion,
    [Required] double GastosAdministracion,
    [Required] double PorcentajeSeguroDegravamen,
    [Required] double PorcentajeSeguroRiesgo,
    [Required] int PlazoOperacion,
    [Required] DateTime FechaDescuento,
    [Required] bool pagoFueraDeFecha,
    int DiasDespuesDelVencimiento,
    double comisionDePagoTardio,
    string PeriodoTasaMoratorio,
    double tasaDeInteresMoratorio,
    string TipoTasaInteresMoratorio,
    string PeriodoCapitalizaciondeTasaMoratoria
    );