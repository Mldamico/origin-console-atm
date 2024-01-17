using OriginConsole.Models;

namespace OriginConsole.Views;

public class Home
{
    private readonly CuentaTarjeta _cuentaTarjeta;

    public Home(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
    }

    public async Task Display()
    {
        Console.WriteLine("1 - Balance");
        Console.WriteLine("2 - Retiro");
        Console.WriteLine("3 - Reporte");
        Console.WriteLine("4 - Salir");

        while (true)
        {
            var choice  = Console.ReadKey();
            Console.WriteLine("");
            switch(choice.Key)
            {
                case ConsoleKey.D1:
                    var balance = new Balance(_cuentaTarjeta);
                    await balance.Display();
                    break;
                case ConsoleKey.D2:
                    var retiro = new Retiro(_cuentaTarjeta);
                    await retiro.Display();
                    break;
                case ConsoleKey.D3:
                    var reporte = new Reporte(_cuentaTarjeta);
                    await reporte.Display();
                    break;
                case ConsoleKey.D4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            
            };
        }
    }
    
}