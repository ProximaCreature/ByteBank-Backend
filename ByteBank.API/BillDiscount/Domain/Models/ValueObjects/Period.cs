namespace ByteBank.API.BillDiscount.Domain.Models.ValueObjects;

public enum Period
{
    ANUAL = 360,
    SEMESTRAL = 180,
    CUATRIMESTRAL = 120,
    TRIMESTRAL = 90,
    BIMESTRAL = 60,
    MENSUAL = 30,
    QUINCENAL = 15,
    SEMANAL = 7,
    DIARIO = 1
}