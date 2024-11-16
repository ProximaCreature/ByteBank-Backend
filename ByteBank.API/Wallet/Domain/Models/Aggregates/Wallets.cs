using ByteBank.API.BillDiscount.Application.Features;
using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Models.ValueObjects;
using ByteBank.API.Shared.Domain.Models.Entities;

namespace ByteBank.API.Wallet.Domain.Models.Aggregates;

public class Wallets : BaseDomainModel
{
    public required string NombreCartera { get; set; }
    public required double TasaInteres { get; set; }
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
            case "ninguno":
                return 0;
            default:
                throw new ArgumentException("Período no reconocido");
        }
    }

    public double CalcularTEA()
    {
        int periodosPorAnoTasa = DeterminarPeriodosPorAno(PeriodoTasa); 
        int periodosPorAnoCapitalizacion = DeterminarPeriodosPorAno(PeriodoCapitalizacion);

        double tasadouble = TasaInteres / 100;

        if (TipoTasaInteres.ToLower() == "nominal")
        {
            double tasaConvertida = tasadouble / (periodosPorAnoTasa / periodosPorAnoCapitalizacion);
            double TEA = Math.Pow((1 + tasaConvertida), (12/ periodosPorAnoCapitalizacion) - 1);
            Console.WriteLine($"TEA Calculada desde Tasa Nominal: {TEA * 100}%");
            return TEA;
        }
        else if (TipoTasaInteres.ToLower() == "efectiva")
        {
            double TEA = Math.Pow((1 + tasadouble), 12/periodosPorAnoTasa) - 1;
            Console.WriteLine($"TEA Calculada desde Tasa Efectiva: {TEA * 100}%");
            return TEA;
        }
        else
        {
            throw new ArgumentException("Tipo de tasa de interés no reconocido");
        }
    }
    public required double ComisionActivacionPorLetra { get; set; }
    public required double Portes { get; set; }
    private double _porcentajeRetencion;
    public required double PorcentajeRetencion
    {
        get => _porcentajeRetencion;
        set => _porcentajeRetencion = value / 100;  // Convertir a double
    }
    public required double GastosAdministracion { get; set; }

    private double _porcentajeSeguroDegravamen;
    public required double PorcentajeSeguroDegravamen
    {
        get => _porcentajeSeguroDegravamen;
        set => _porcentajeSeguroDegravamen = value / 100;  // Convertir a double
    }

    private double _porcentajeSeguroRiesgo;
    public required double PorcentajeSeguroRiesgo
    {
        get => _porcentajeSeguroRiesgo;
        set => _porcentajeSeguroRiesgo = value / 100;  // Convertir a double
    }
    public required int PlazoOperacion { get; set; }
    public required DateTime FechaDescuento { get; set; }
    
    public required bool pagoFueraDeFecha { get; set; }
    public int DiasDespuesDelVencimiento { get; set; }
    public double comisionDePagoTardio { get; set; }
    public string PeriodoTasaMoratorio { get; set; }
    public double tasaDeInteresMoratorio { get; set; }
    public string TipoTasaInteresMoratorio { get; set; }
    public string PeriodoCapitalizaciondeTasaMoratoria { get; set; }
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public double CalcularValorRecibido()
    {
        try
        {
            if (!Bills.Any(bill => bill.WalletId == Id))
            {
                return 0;
            }

            double ValorRecibidoTotal = 0;
            foreach (var bill in Bills.Where(b => b.WalletId == Id))
            {
                var expirationDate = bill.ExpirationDate;

                var dias = (FechaDescuento - expirationDate).Days;
                double plazofinal = PlazoOperacion - dias;
                Console.WriteLine(
                    $"Bill ID: {bill.Id}, Expiration Date: {expirationDate}, Days Difference: {dias}, Plazo Final: {plazofinal}");

                double TEACalculo = CalcularTEA();
                double TEC = Math.Pow(1.0 + TEACalculo, plazofinal / 360) - 1;
                Console.WriteLine($"Bill ID: {bill.Id}, TEC: {TEC}");
                double TECValor = TEC / (1 + TEC);
                Console.WriteLine($"TEC Valor: {TECValor}");

                var faceValue = bill.FaceValue;

                var Descuento = faceValue * TECValor;
                double valorNeto = faceValue - Descuento;
                Console.WriteLine(
                    $"Bill ID: {bill.Id}, Face Value: {faceValue}, Descuento: {Descuento}, Valor Neto: {valorNeto}");

                var ValorRecibido = valorNeto - ComisionActivacionPorLetra -
                                    (faceValue * PorcentajeSeguroRiesgo) -
                                    (faceValue * PorcentajeSeguroDegravamen) -
                                    (faceValue * PorcentajeRetencion);
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

    public double CalcularValorEntregado()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        double ValorEntregadoTotal = 0;
        foreach (var bill in Bills.Where(b => b.WalletId == Id))
        {
            var faceValue = bill.FaceValue;
            var ValorEntregado = faceValue + GastosAdministracion + Portes -
                                 (faceValue * PorcentajeRetencion);
            ValorEntregadoTotal += ValorEntregado;
            Console.WriteLine($"Bill ID: {bill.Id}, Face Value: {faceValue}, Valor Entregado: {ValorEntregado}");

        }
        Console.WriteLine($"Total Valor Entregado: {ValorEntregadoTotal}");
        return ValorEntregadoTotal;
    }

    public double CalcularValorEntregadoConMora()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }
        
        Period periodoTasaMoratorio = toPeriodEnumFromString(PeriodoTasaMoratorio);
        Period periodoCapitalizaciondeTasaMoratorio = toPeriodEnumFromString(PeriodoCapitalizaciondeTasaMoratoria);
        
        //Convirtiendo tasa nominal a tasa efectiva
        if (TipoTasaInteresMoratorio.ToLower() == "nominal")
        {
            tasaDeInteresMoratorio = FinancialOperation.ConvertToNewTEPFromTN(tasaDeInteresMoratorio, periodoTasaMoratorio, (int) periodoTasaMoratorio, periodoCapitalizaciondeTasaMoratorio);
        }

        double ValorEntregadoTotal = 0;
        foreach (var bill in Bills.Where(b => b.WalletId == Id))
        {
            var interesCompensatorio = FinancialOperation.CalculateCompensatoryInterest(
                bill.FaceValue,
                TasaInteres,
                toPeriodEnumFromString(PeriodoTasa),
                DiasDespuesDelVencimiento
            );

            var interesMoratorio = FinancialOperation.CalculateMoratoriumInterest(
                bill.FaceValue,
                tasaDeInteresMoratorio,
                periodoTasaMoratorio,
                DiasDespuesDelVencimiento
            );
            var gastosTotales = comisionDePagoTardio + interesMoratorio + interesCompensatorio + GastosAdministracion + Portes;
            
            var valorEntregado = FinancialOperation.CalculateDeliveredValue(
                bill.FaceValue,
                gastosTotales,
                _porcentajeRetencion
            );
            
            ValorEntregadoTotal += valorEntregado;
        }
        
        return ValorEntregadoTotal;
    }

    public double TCEAconMora()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        var PlazoOperacionTardio = PlazoOperacion + DiasDespuesDelVencimiento;
        var ValorTCEA = Math.Pow((CalcularValorEntregadoConMora() / CalcularValorRecibido()), (360 / PlazoOperacionTardio)) - 1;
        Console.WriteLine($"TCEA: {ValorTCEA}");
        return ValorTCEA;
    }

    public Period toPeriodEnumFromString(string periodo)
    {
        switch (periodo.ToLower())
        {
            case "anual":
                return Period.ANUAL;
            case "semestral":
                return Period.SEMESTRAL;
            case "cuatrimestral":
                return Period.CUATRIMESTRAL;
            case "trimestral":
                return Period.TRIMESTRAL;
            case "bimestral":
                return Period.BIMESTRAL;
            case "mensual":
                return Period.MENSUAL;
            default:
                throw new ArgumentException("Período no reconocido");
        }
    }

    public double TCEA()
    {
        if (!Bills.Any(bill => bill.WalletId == Id))
        {
            return 0;
        }

        var ValorTCEA = Math.Pow((CalcularValorEntregado() / CalcularValorRecibido()), (360 / PlazoOperacion)) - 1;
        Console.WriteLine($"TCEA: {ValorTCEA}");
        return ValorTCEA;
    }
}