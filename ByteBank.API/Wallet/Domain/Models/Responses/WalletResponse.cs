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
    public decimal Tcea { get; init; }
    public decimal ValorRecibido { get; init; }
    public decimal ValorEntregado { get; init; }
    public DateTime FechaDescuento { get; init; }
    public int PlazoOperacion { get; init; }
    public bool HasAssociatedBill { get; init; }
}