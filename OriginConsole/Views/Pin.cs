using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Servicios;

namespace OriginConsole.Views;

public class Pin: IView
{
    private readonly Tarjeta _creditCard;
    private readonly IAccountRepository _accountRepository;
    private readonly ICardRepository _cardRepository;
    public Pin(Tarjeta creditCard)
    {
        _creditCard = creditCard;
        _accountRepository = new AccountRepository();
        _cardRepository = new CardRepository();
    }

    public async Task Display()
    {
        CuentaTarjeta? cuenta;
        while (true)
        {
            Console.WriteLine("Ingrese el pin");
            var pin = Console.ReadLine();
            cuenta = await  _accountRepository.Login(pin);
            if(cuenta is not null) break;
      
            if (cuenta is null)
            {
                _creditCard.intentos_restantes = _creditCard.intentos_restantes - 1;
                if (_creditCard.intentos_restantes <= 0)
                {
                    _creditCard.Bloqueada = 1;
                    await _cardRepository.UpdateCard(_creditCard);
                    Console.WriteLine("Cuenta bloqueada");
                    Environment.Exit(0);
                }
                await _cardRepository.UpdateCard(_creditCard);
              
                Console.WriteLine(_creditCard);
            }    
        }

        if (_creditCard.intentos_restantes != 4)
        {
            _creditCard.intentos_restantes = 4;
            await _cardRepository.UpdateCard(_creditCard);
        }

        var home = new Home(cuenta);

        await home.Display();

    }
    
}