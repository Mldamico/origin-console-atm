using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Servicios;

namespace OriginConsole.Home;

public class Retiro
{
    private IOperationRepository _operationRepository;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private ICardRepository _cardRepository;
    public Retiro(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _cardRepository = new CardRepository();
        _operationRepository = new OperationRepository();
    }

    public async Task DisplayMessage()
    {
        Console.WriteLine($"Cuanto desea retirar. Puede un maximo de ${_cuentaTarjeta.Saldo}");
        

        while (true)
        {
            decimal amount = Decimal.Parse(Console.ReadLine());
            if (amount < 0)
            {
                amount = 0;
            } 
            if (_cuentaTarjeta.Saldo - amount < 0)
            {
                Console.WriteLine($"El monto no puede ser superior al saldo. Usted puede retirar un maximo de ${_cuentaTarjeta.Saldo}");
            }
            else
            {
                Console.WriteLine("Entro aca");
                _cuentaTarjeta.Saldo = _cuentaTarjeta.Saldo - amount;
                Console.WriteLine(_cuentaTarjeta.Saldo);
                Tarjeta newCard = new Tarjeta()
                {
                    fecha_vencimiento = _cuentaTarjeta.fecha_vencimiento,
                    Id = _cuentaTarjeta.Id,
                    intentos_restantes = _cuentaTarjeta.intentos_restantes,
                    Bloqueada = _cuentaTarjeta.Bloqueada,
                    Numero = _cuentaTarjeta.Numero,
                    Saldo = _cuentaTarjeta.Saldo,
                    CuentaId = _cuentaTarjeta.CuentaId
                };
                await _cardRepository.UpdateCard(newCard);
                await _operationRepository.RegisterWithdraw(newCard.Id, amount);
                break;
            }
            
            
        }
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