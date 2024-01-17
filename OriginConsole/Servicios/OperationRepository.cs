using System.Data.SqlClient;
using Dapper;
using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Models.Dto;

namespace OriginConsole.Servicios;

public class OperationRepository: IOperationRepository
{
    private readonly string connectionString;
    public OperationRepository()
    {
        connectionString =  "server=localhost; database=origin; user id=sa; password=reallyStrongPwd123; Encrypt=false;";
    }

    public async Task RegisterBalance(int id)
    {
        using var connection = new SqlConnection(connectionString);
        var fecha = DateTime.Now;
        var tipo_operacion = await connection.QueryFirstOrDefaultAsync<TipoOperacion>("SELECT id from tipo_operacion WHERE detalle = @detalle", new {detalle = "Balance"});
        var tarjeta_id = id;
        await connection.ExecuteAsync(@"INSERT INTO operacion (fecha, tipo_operacion_id, tarjeta_id) values(@fecha, @tipo_operacion_id, @tarjeta_id);", new { fecha, tipo_operacion_id = tipo_operacion.Id, tarjeta_id });
        
    }

    public async Task RegisterWithdraw(int id, decimal monto)
    {
        using var connection = new SqlConnection(connectionString);
        var fecha = DateTime.Now;
        var tipo_operacion = await connection.QueryFirstOrDefaultAsync<TipoOperacion>("SELECT id from tipo_operacion WHERE detalle = @detalle", new {detalle = "Balance"});
        var tarjeta_id = id;
        await connection.ExecuteAsync(@"INSERT INTO operacion (fecha, tipo_operacion_id, tarjeta_id, monto) values(@fecha, @tipo_operacion_id, @tarjeta_id, @monto);", new { fecha, tipo_operacion_id = tipo_operacion.Id, tarjeta_id, monto });
    }

    public async Task<IEnumerable<ReporteDto>> GetReporte(int id)
    {
        using var connection = new SqlConnection(connectionString);
        var reporte = await connection.QueryAsync<ReporteDto>(@"select  numero, fecha ,monto, (monto + saldo) as total from operacion inner join tarjeta t on t.id = operacion.tarjeta_id inner join tipo_operacion tipo on tipo.id = operacion.tipo_operacion_id where t.id = @id;", new{id});
        return reporte;
    }
}