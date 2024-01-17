using System.Data.SqlClient;
using Dapper;

using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Utils;

namespace OriginConsole.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string _connectionString;
    
    public AccountRepository()
    {
        
        _connectionString = Configuration.ConnectionString;
         
         
    }

    public async Task<CuentaTarjeta> Login(string pin)
    {
        using var connection = new SqlConnection(_connectionString);
        var cuenta = await connection.QueryFirstOrDefaultAsync<CuentaTarjeta>(@"select * from cuenta inner join tarjeta t on t.cuenta_id = cuenta.id where pin = @pin; ", new {pin});

        return cuenta;
    }
    
    
}