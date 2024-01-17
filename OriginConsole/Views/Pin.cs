using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Repositories;
using OriginConsole.Services;

namespace OriginConsole.Views;

public class Pin: IView
{
    private readonly Tarjeta _creditCard;
    private readonly IAccountService _accountService;
    private readonly ICardService _cardService;
    public Pin(Tarjeta creditCard)
    {
        _creditCard = creditCard;
        _accountService = new AccountService();
        _cardService = new CardService();
    }

    public async Task Display()
    {
        CuentaTarjeta? cuenta;
        while (true)
        {
            Console.WriteLine("Ingrese el pin");
            var pin = Console.ReadLine();
            cuenta = await  _accountService.Login(pin);
            if(cuenta is not null) break;
      
            if (cuenta is null)
            {
                _creditCard.intentos_restantes -= 1;
                if (_creditCard.intentos_restantes <= 0)
                {
                    _creditCard.Bloqueada = 1;
                    await _cardService.UpdateCard(_creditCard);
                    Console.WriteLine("Cuenta bloqueada");
                    Environment.Exit(0);
                }
                await _cardService.UpdateCard(_creditCard);
              
               
            }    
        }

        if (_creditCard.intentos_restantes != 4)
        {
            _creditCard.intentos_restantes = 4;
            await _cardService.UpdateCard(_creditCard);
        }

        var home = new Home(cuenta);

        await home.Display();

    }
    
}