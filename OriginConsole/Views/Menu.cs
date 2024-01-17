using OriginConsole.Interfaces;
using OriginConsole.Models;

namespace OriginConsole.Home;

public class Menu
{
    private readonly ICardRepository _cardRepository;
    private string tarjeta { get; set; }

    public Menu(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task ShowMenu()
    {
        Tarjeta tarjetaEncontrada;
        while (true)
        {
            Console.WriteLine("Ingrese numero de tarjeta");
            tarjeta = Console.ReadLine();
            tarjetaEncontrada =await _cardRepository.FindCard(tarjeta);
            if (tarjetaEncontrada is not null)
            {
                break;
            }
            
        }
        var newPin = new Pin(tarjetaEncontrada);

        await newPin.displayMessage();
        
    }
    
}