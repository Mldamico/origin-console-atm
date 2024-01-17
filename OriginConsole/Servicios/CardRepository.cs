using System.Data.SqlClient;
using Dapper;
using OriginConsole.Home;
using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Models.Dto;

namespace OriginConsole.Servicios;

public class CardRepository :ICardRepository
{
    private readonly string connectionString;
    public CardRepository()
    {
        connectionString =  "server=localhost; database=origin; user id=sa; password=reallyStrongPwd123; Encrypt=false;";
    }

    public async Task<Tarjeta> FindCard(string numero)
    {
        using var connection = new SqlConnection(connectionString);
      
        
        var card = await connection.QueryFirstOrDefaultAsync<Tarjeta>(@"select * from tarjeta where numero = @numero", new {numero});
        
        if (card is null)
        {
            Console.WriteLine("Tarjeta no encontrada");
            return null;
        }
        
        
        return card;
        
    }

    public async Task UpdateCard(Tarjeta newCard)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(@"UPDATE tarjeta SET intentos_restantes = @intentos_restantes, bloqueada = @Bloqueada, saldo= @saldo where id = @Id;",newCard);
    }
    
    public async Task<BalanceDto> GetBalance(int id)
    {
        using var connection = new SqlConnection(connectionString);
        
        var balance = await connection.QueryFirstOrDefaultAsync<BalanceDto>(@"select numero, saldo, fecha_vencimiento from tarjeta inner join cuenta c on c.id = tarjeta.cuenta_id where tarjeta.id = @id;", new {id});

        return balance;
    }
    
    
   
    
    
}