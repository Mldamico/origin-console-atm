namespace OriginConsole.Models;

public class OperacionTarjeta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal? Monto { get; set; }
    public int OperacionId { get; set; }
    public int TarjetaId { get; set; }
    public string Numero { get; set; }
    public int CuentaId { get; set; }
    public int Bloqueada { get; set; }
    public int intentos_restantes { get; set; }
    public string Pin { get; set; }
    public Decimal Saldo { get; set; }
    public string Detalle { get; set; }
    public DateTime fecha_vencimiento { get; set; }
}