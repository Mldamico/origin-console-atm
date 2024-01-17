using System.Data.SqlClient;
using Dapper;
using OriginConsole.Home;
using OriginConsole.Interfaces;
using OriginConsole.Models;

namespace OriginConsole.Servicios;

public class AccountRepository : IAccountRepository
{
    private readonly string connectionString;
    
    public AccountRepository()
    {
        connectionString =  "server=localhost; database=origin; user id=sa; password=reallyStrongPwd123; Encrypt=false;";
    }

    public async Task<CuentaTarjeta> Login(string pin)
    {
        using var connection = new SqlConnection(connectionString);
        var cuenta = await connection.QueryFirstOrDefaultAsync<CuentaTarjeta>(@"select * from cuenta inner join tarjeta t on t.cuenta_id = cuenta.id where pin = @pin; ", new {pin});
        
        if (cuenta is null)
        {
            return null;
        }
        if (cuenta.Bloqueada == 1)
        {
            Console.WriteLine("Cuenta bloqueada");
            Environment.Exit(0);
        }
        
        Console.WriteLine(cuenta.intentos_restantes);

        

        return cuenta;
    }
    
    
}