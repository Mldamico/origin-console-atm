namespace OriginConsole.Models;

public class CuentaTarjeta
{
    public int Id { get; set; }
    public string Numero { get; set; }
    public int CuentaId { get; set; }
    public int Bloqueada { get; set; }
    public int intentos_restantes { get; set; }
    public string Pin { get; set; }
    public Decimal Saldo { get; set; }
    public DateTime fecha_vencimiento { get; set; }
}