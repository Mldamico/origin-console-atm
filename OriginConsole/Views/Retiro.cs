using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Services;
using OriginConsole.Utils;

namespace OriginConsole.Views;

public class Retiro :IView
{
    private IOperationService _operationService;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private ICardService _cardService;
    private Print _print;
    public Retiro(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _cardService = new CardService();
        _operationService = new OperationService();
        _print = new Print(cuentaTarjeta);
    }

    public async Task Display()
    {
        Console.WriteLine($"Cuanto desea retirar. Puede un maximo de ${_cuentaTarjeta.Saldo}");
        
        while (true)
        {
            string value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
            {
                value = "0";
            }
            decimal amount = Decimal.Parse(value);
            
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
                var previousAmount = _cuentaTarjeta.Saldo;
                
                _cuentaTarjeta.Saldo -= amount;
                var tarjeta = MapTarjeta(_cuentaTarjeta);
                await _cardService.UpdateCard(tarjeta);
                await _operationService.RegisterWithdraw(_cuentaTarjeta.Id, amount, previousAmount);
                break;
            }
            
            
        }
        await _print.ShowBackOptions();
        
        
    }


    private Tarjeta MapTarjeta(CuentaTarjeta cuentaTarjeta)
    {
        Tarjeta newCard = new Tarjeta()
        {
            fecha_vencimiento = cuentaTarjeta.fecha_vencimiento,
            Id = cuentaTarjeta.Id,
            intentos_restantes = cuentaTarjeta.intentos_restantes,
            Bloqueada = cuentaTarjeta.Bloqueada,
            Numero = cuentaTarjeta.Numero,
            Saldo = cuentaTarjeta.Saldo,
            CuentaId = cuentaTarjeta.CuentaId
        };

        return newCard;
    } 
    
}