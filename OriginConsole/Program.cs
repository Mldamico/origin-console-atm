// See https://aka.ms/new-console-template for more information

using OriginConsole.Home;
using OriginConsole.Interfaces;
using OriginConsole.Servicios;


ICardRepository cardRepository = new CardRepository();

var initApp = new Init(cardRepository);
await initApp.Execute();


// using var connection = new SqlConnection(connectionString);
//
// var result = await connection.QueryFirstOrDefaultAsync<Cuenta>(@"SELECT * from cuenta");

// Console.WriteLine(result.Pin);
