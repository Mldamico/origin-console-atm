using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Servicios;
using OriginConsole.Utils;

namespace OriginConsole.Views;

public class Balance
{
    private IOperationRepository _operationRepository;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private ICardRepository _cardRepository;
    private Print _print;
    public Balance(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _cardRepository = new CardRepository();
        _operationRepository = new OperationRepository();
        _print = new Print(cuentaTarjeta);
    }

    public async Task ShowBalance()
    {
        Console.WriteLine("El Balance es: ");
        var balance = await _cardRepository.GetBalance(_cuentaTarjeta.Id);
       
        Console.WriteLine($"La tarjeta numero: {balance.Numero} posee un saldo de ${balance.Saldo} y vence el {balance.fecha_vencimiento}");
        await _operationRepository.RegisterBalance(_cuentaTarjeta.Id);

        await _print.ShowBackOptions();
    }
    
    
}
