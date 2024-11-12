using ByteBank.API.Shared.Domain.Models.Entities;

namespace ByteBank.API.Wallet.Domain.Models.Aggregates;

public class Wallets : BaseDomainModel
{
    public required string NombreCartera { get; set; }
    public required decimal TasaInteres { get; set; }
    public required string TipoTasaInteres { get; set; }
    public required string PeriodoTasa { get; set; }
    public required string PeriodoCapitalizacion { get; set; }
    public required decimal ComisionActivacionPorLetra { get; set; }
    public required decimal Portes { get; set; }
    public required decimal PorcentajeRetencion { get; set; }
    public required decimal GastosAdministracion { get; set; }
    public required decimal PorcentajeSeguroDegravamen { get; set; }
    public required decimal PorcentajeSeguroRiesgo { get; set; }
    public required int PlazoOperacion { get; set; }
    public required DateTime FechaDescuento { get; set; }
}