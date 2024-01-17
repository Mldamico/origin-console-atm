using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Servicios;

namespace OriginConsole.Home;

public class Balance
{
    private IOperationRepository _operationRepository;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private ICardRepository _cardRepository;
    public Balance(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _cardRepository = new CardRepository();
        _operationRepository = new OperationRepository();
    }

    public async Task ShowBalance()
    {
        Console.WriteLine("El Balance es: ");
        var balance = await _cardRepository.GetBalance(_cuentaTarjeta.Id);
       
        Console.WriteLine($"La tarjeta numero: {balance.Numero} posee un saldo de ${balance.Saldo} y vence el {balance.fecha_vencimiento}");
        await _operationRepository.RegisterBalance(_cuentaTarjeta.Id);
        Console.WriteLine("Que desea hacer?");
        Console.WriteLine("1 - Atras");
        Console.WriteLine("2 - Salir");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                var home = new Home(_cuentaTarjeta);
                await home.Display();
                
                break;
            case "2":
                Environment.Exit(0);
                break;
            
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        
    }
    
    
}