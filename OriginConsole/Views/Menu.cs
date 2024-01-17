
using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Utils;

namespace OriginConsole.Views;

public class Menu : IView
{
    private readonly ICardRepository _cardRepository;
    private string tarjeta { get; set; }

    public Menu(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task Display()
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
        Console.WriteLine($"Tarjeta Ingresada: {StringExtensions.FormatCard(tarjetaEncontrada.Numero)}");
        var newPin = new Pin(tarjetaEncontrada);

        await newPin.Display();
        
    }
    
}


