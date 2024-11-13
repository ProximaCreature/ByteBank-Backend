using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.Shared.Domain.Models.Entities;

namespace ByteBank.API.Wallet.Domain.Models.Aggregates;

public class Wallets : BaseDomainModel
{
    public required string NombreCartera { get; set; }
    public required decimal TasaInteres { get; set; }
    public required string TipoTasaInteres { get; set; }
    public required string PeriodoTasa { get; set; }
    public required string PeriodoCapitalizacion { get; set; }
    private int DeterminarPeriodosPorAno(string periodo)
    {
        switch (periodo.ToLower())
        {
            case "anual":
                return 12;
            case "semestral":
                return 6;
            case "cuatrimestral":
                return 4;
            case "trimestral":
                return 3;
            case "bimestral":
                return 2;
            case "mensual":
                return 1;
            default:
                throw new ArgumentException("Período no reconocido");
        }
    }

    public decimal CalcularTEA()
    {
        int periodosPorAnoTasa = DeterminarPeriodosPorAno(PeriodoTasa); 
        int periodosPorAnoCapitalizacion = DeterminarPeriodosPorAno(PeriodoCapitalizacion);

        decimal tasaDecimal = TasaInteres / 100;

        if (TipoTasaInteres.ToLower() == "nominal")
        {
            decimal tasaConvertida = tasaDecimal / (periodosPorAnoTasa / periodosPorAnoCapitalizacion);
            decimal TEA = (decimal)Math.Pow((double)(1 + tasaConvertida), (12/ periodosPorAnoCapitalizacion) - 1);
            Console.WriteLine($"TEA Calculada desde Tasa Nominal: {TEA * 100}%");
            return TEA;
        }
        else if (TipoTasaInteres.ToLower() == "efectiva")
        {
            decimal TEA = (decimal)Math.Pow((double)(1 + tasaDecimal), 12/periodosPorAnoTasa) - 1;
            Console.WriteLine($"TEA Calculada desde Tasa Efectiva: {TEA * 100}%");
            return TEA;
        }
        else
        {
            throw new ArgumentException("Tipo de tasa de interés no reconocido");
        }
    }
    public required decimal ComisionActivacionPorLetra { get; set; }
    public required decimal Portes { get; set; }
    private decimal _porcentajeRetencion;
    public required decimal PorcentajeRetencion
    {
        get => _porcentajeRetencion;
        set => _porcentajeRetencion = value / 100;  // Convertir a decimal
    }
    public required decimal GastosAdministracion { get; set; }

    private decimal _porcentajeSeguroDegravamen;
    public required decimal PorcentajeSeguroDegravamen
    {
        get => _porcentajeSeguroDegravamen;
        set => _porcentajeSeguroDegravamen = value / 100;  // Convertir a decimal
    }

    private decimal _porcentajeSeguroRiesgo;
    public required decimal PorcentajeSeguroRiesgo
    {
        get => _porcentajeSeguroRiesgo;
        set => _porcentajeSeguroRiesgo = value / 100;  // Convertir a decimal
    }
    public required int PlazoOperacion { get; set; }
    public required DateTime FechaDescuento { get; set; }
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();

    /*/public decimal NumDias()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        decimal plazofinal = 0;
        foreach (var bill in Bills.Where(b => b.WalletId == Id))
        {
            var expirationDate = bill.ExpirationDate;

            var dias = (FechaDescuento - expirationDate).Days;
            plazofinal = PlazoOperacion - dias;
            Console.WriteLine($"Bill ID: {bill.Id}, Expiration Date: {expirationDate}, Days Difference: {dias}, Plazo Final: {plazofinal}");
        }

        return plazofinal;
    }
    
    public decimal CalcularDDia()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        decimal TEC = 0;
        decimal TECValor = 0;
        foreach (var bill in Bills.Where(b => b.WalletId == Id))
        {
            TEC = (decimal)Math.Pow(1.0 + (double)TasaInteres, (double)NumDias() / 360) - 1;
            Console.WriteLine($"Bill ID: {bill.Id}, TEC: {TEC}");
            TECValor = TEC / (1 + TEC);
            Console.WriteLine($"TEC Valor: {TECValor}");
        }
        return TECValor;
    }

    public decimal CalcularValorNeto()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }
        decimal valorNeto = 0;
        foreach (var bill in Bills.Where(b => b.WalletId == Id))
        {
            var faceValue = bill.FaceValue;

            var Descuento = (decimal)faceValue * CalcularDDia();
            valorNeto = (decimal)faceValue - (decimal)Descuento;
            Console.WriteLine($"Bill ID: {bill.Id}, Face Value: {faceValue}, Descuento: {Descuento}, Valor Neto: {valorNeto}");

        }

        return valorNeto;
    }/*/

    public decimal CalcularValorRecibido()
    {
        try
        {
            if (!Bills.Any(bill => bill.WalletId == Id))
            {
                return 0;
            }

            decimal ValorRecibidoTotal = 0;
            foreach (var bill in Bills.Where(b => b.WalletId == Id))
            {
                var expirationDate = bill.ExpirationDate;

                var dias = (FechaDescuento - expirationDate).Days;
                decimal plazofinal = PlazoOperacion - dias;
                Console.WriteLine(
                    $"Bill ID: {bill.Id}, Expiration Date: {expirationDate}, Days Difference: {dias}, Plazo Final: {plazofinal}");

                decimal TEACalculo = CalcularTEA();
                decimal TEC = (decimal)Math.Pow(1.0 + (double)TEACalculo, (double)plazofinal / 360) - 1;
                Console.WriteLine($"Bill ID: {bill.Id}, TEC: {TEC}");
                decimal TECValor = TEC / (1 + TEC);
                Console.WriteLine($"TEC Valor: {TECValor}");

                var faceValue = bill.FaceValue;

                var Descuento = (decimal)faceValue * TECValor;
                decimal valorNeto = (decimal)faceValue - (decimal)Descuento;
                Console.WriteLine(
                    $"Bill ID: {bill.Id}, Face Value: {faceValue}, Descuento: {Descuento}, Valor Neto: {valorNeto}");

                var ValorRecibido = valorNeto - ComisionActivacionPorLetra -
                                    ((decimal)faceValue * PorcentajeSeguroRiesgo) -
                                    ((decimal)faceValue * PorcentajeSeguroDegravamen) -
                                    ((decimal)faceValue * PorcentajeRetencion);
                ValorRecibidoTotal += ValorRecibido;
                Console.WriteLine($"Bill ID: {bill.Id}, Face Value: {faceValue}, Valor Recibido: {ValorRecibido}");
            }

            Console.WriteLine($"Total Valor Recibido: {ValorRecibidoTotal}");
            return ValorRecibidoTotal;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public decimal CalcularValorEntregado()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        decimal ValorEntregadoTotal = 0;
        foreach (var bill in Bills.Where(b => b.WalletId == Id))
        {
            var faceValue = bill.FaceValue;
            var ValorEntregado = (decimal)faceValue + GastosAdministracion + Portes -
                                 ((decimal)faceValue * PorcentajeRetencion);
            ValorEntregadoTotal += ValorEntregado;
            Console.WriteLine($"Bill ID: {bill.Id}, Face Value: {faceValue}, Valor Entregado: {ValorEntregado}");

        }
        Console.WriteLine($"Total Valor Entregado: {ValorEntregadoTotal}");
        return ValorEntregadoTotal;
    }

    public decimal TCEA()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        var ValorTCEA = Math.Pow((double)(CalcularValorEntregado() / CalcularValorRecibido()), (360 / PlazoOperacion)) - 1;
        Console.WriteLine($"TCEA: {ValorTCEA}");
        return (decimal)ValorTCEA;
    }
}