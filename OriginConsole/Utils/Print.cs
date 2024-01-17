using OriginConsole.Models;
using OriginConsole.Views;

namespace OriginConsole.Utils;


public class Print(CuentaTarjeta cuentaTarjeta)
{
    public async Task ShowBackOptions()
    {
        Console.WriteLine("Que desea hacer?");
        Console.WriteLine("1 - Atras");
        Console.WriteLine("2 - Salir");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                var home = new Home(cuentaTarjeta);
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