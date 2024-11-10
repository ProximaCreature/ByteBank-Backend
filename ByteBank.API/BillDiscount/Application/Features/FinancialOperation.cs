using ByteBank.API.BillDiscount.Domain.Models.ValueObjects;

namespace ByteBank.API.BillDiscount.Application.Features;

public class FinancialOperation
{
    public static double ConvertToNewTEPFromTEP(double sourceTEP, Period sourcePeriod, int destinationPeriodDays)
    {
        var destinationTEP = Math.Pow(1d+sourceTEP, destinationPeriodDays/(double) sourcePeriod) - 1d;
        return destinationTEP;
    }

    public static double ConvertToNewTEPFromTN(double sourceTN, Period sourcePeriod, int destinationPeriodDays, Period capitalizationPeriod = Period.DIARIO)
    {
        var destinationTEP = Math.Pow(1d + sourceTN / ((double) sourcePeriod/ (double) capitalizationPeriod), destinationPeriodDays/(double) capitalizationPeriod) - 1d;
        return destinationTEP;
    }

    public static double CalculateNetValue(double faceValue, double TEP)
    {
        var discountRate = TEP/(1d+TEP);
        var discount = discountRate * faceValue;
        var netValue = faceValue - discount;
        return netValue;
    }

    public static double CalculateReceivedValue(double faceValue, double netValue, double totalExpenses = 0, double degravamenRate = 0, double retentionRate = 0)
    {
        var degravamen = degravamenRate * faceValue;
        var retention = retentionRate * faceValue;
        var receivedValue = netValue -(totalExpenses + degravamen  + retention);
        return receivedValue;
    }

    public static double CalculateDeliveredValue(double faceValue, double totalExpenses = 0, double retentionRate = 0)
    {
        var retention = retentionRate * faceValue;
        var deliveredValue = faceValue + totalExpenses - retention;
        return deliveredValue;
    }

    public static double CalculateTCEA(double receivedValue, double deliveredValue, int daysToTransfer)
    {
        var TCEA = Math.Pow(deliveredValue/receivedValue, 360d/daysToTransfer)-1;
        return TCEA;
    }

    public static double CalculateCompensatoryInterest(double faceValue, double TEPCompensatory, Period compensatoryPeriod, int daysToTransfer)
    {
        var compensatoryInterest = faceValue * (Math.Pow(1d + TEPCompensatory, daysToTransfer/(double) compensatoryPeriod)-1d);
        return compensatoryInterest;
    }

    public static double CalculateMoratoriumInterest(double faceValue, double TEPMoratorium, Period moratoriumPeriod, int daysToTransfer)
    {
        var moratoriumInterest = faceValue * (Math.Pow(1 + TEPMoratorium, daysToTransfer/ (double) moratoriumPeriod) - 1);
        return moratoriumInterest;
    }
}