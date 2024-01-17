using System.Data.SqlClient;
using Dapper;
using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Utils;

namespace OriginConsole.Repositories;

public class OperationRepository: IOperationRepository
{
    private readonly string _connectionString;
    public OperationRepository()
    {
        _connectionString =  Configuration.ConnectionString;
    }

    public async Task RegisterBalance(int id, decimal previousBalance)
    {
        using var connection = new SqlConnection(_connectionString);
        var fecha = DateTime.Now;
        var tipo_operacion = await connection.QueryFirstOrDefaultAsync<TipoOperacion>("SELECT id from tipo_operacion WHERE detalle = @detalle", new {detalle = "Balance"});
        var tarjeta_id = id;
        await connection.ExecuteAsync(@"INSERT INTO operacion (fecha, tipo_operacion_id, tarjeta_id, saldo_previo) values(@fecha, @tipo_operacion_id, @tarjeta_id, @previousBalance);", new { fecha, tipo_operacion_id = tipo_operacion.Id, tarjeta_id, previousBalance });
        
    }

    public async Task RegisterWithdraw(int id, decimal amount, decimal previousAmount)
    {
        using var connection = new SqlConnection(_connectionString);
        var fecha = DateTime.Now;
        var tipo_operacion = await connection.QueryFirstOrDefaultAsync<TipoOperacion>("SELECT id from tipo_operacion WHERE detalle = @detalle", new {detalle = "Retiro"});
        var tarjeta_id = id;
        await connection.ExecuteAsync(@"INSERT INTO operacion (fecha, tipo_operacion_id, tarjeta_id, monto, saldo_previo) values(@fecha, @tipo_operacion_id, @tarjeta_id, @monto, @saldo_previo);", new { fecha, tipo_operacion_id = tipo_operacion.Id, tarjeta_id, monto=amount, saldo_previo=previousAmount });
    }

    public async Task<IEnumerable<ReporteDto>> GetReporte(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var reporte = await connection.QueryAsync<ReporteDto>(@"select  numero, fecha ,monto, saldo_previo,(saldo_previo - coalesce(monto,0)) as saldo, tipo.detalle from operacion inner join tarjeta t on t.id = operacion.tarjeta_id inner join tipo_operacion tipo on tipo.id = operacion.tipo_operacion_id where t.id = @id;", new{id});
        return reporte;
    }
}