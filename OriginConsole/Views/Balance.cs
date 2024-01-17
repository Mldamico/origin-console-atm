using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Repositories;
using OriginConsole.Services;
using OriginConsole.Utils;

namespace OriginConsole.Views;

public class Balance: IView
{
    private ICardService _cardService;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private IOperationService _operationService;
    private Print _print;
    public Balance(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _operationService = new OperationService();
        _print = new Print(cuentaTarjeta);
        _cardService = new CardService();
    }

    public async Task Display()
    {
        Console.WriteLine("El Balance es: ");
        var balance = await GetBalance();
        Console.WriteLine($"La tarjeta numero: {balance.Numero} posee un saldo de ${balance.Saldo} y vence el {balance.fecha_vencimiento}");
        await SaveBalance(balance.Saldo);
        await _print.ShowBackOptions();
    }

    private async Task<BalanceDto> GetBalance()
    {
        return await _cardService.GetBalance(_cuentaTarjeta.Id);
    }

    private async Task SaveBalance(decimal balance)
    {
        await _operationService.RegisterBalance(_cuentaTarjeta.Id, balance);
    }
}
