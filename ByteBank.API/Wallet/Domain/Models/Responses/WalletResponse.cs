using ByteBank.API.BillDiscount.Domain.Models.Responses;

namespace ByteBank.API.Wallet.Domain.Models.Responses;
public record WalletResponse
{
    public WalletResponse() { }

    public WalletResponse(string nombreCartera, DateTime fechaDescuento)
    {
        NombreCartera = nombreCartera;
        FechaDescuento = fechaDescuento;
    }

    public int Id { get; init; }
    public string NombreCartera { get; init; }
    public double Tcea { get; init; }
    public double ValorRecibido { get; init; }
    public double ValorEntregado { get; init; }
    public DateTime FechaDescuento { get; init; }
    public int PlazoOperacion { get; init; }
    public bool HasAssociatedBill { get; init; }
    public List<BillResponse> Bills { get; init; } = new();

}