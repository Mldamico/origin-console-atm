namespace OriginConsole.Models.Dto;

public class ReporteDto
{
    public string Numero { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
    public decimal saldo_previo { get; set; }
    public decimal saldo { get; set; }
    public string Detalle { get; set; }
}