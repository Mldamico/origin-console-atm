namespace OriginConsole.Models;

public class Operacion
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal? Monto { get; set; }
    public int OperacionId { get; set; }
    public int TarjetaId { get; set; }
}