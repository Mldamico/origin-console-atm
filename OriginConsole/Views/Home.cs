using OriginConsole.Models;

namespace OriginConsole.Home;

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
            string choice  = Console.ReadLine();
            
            switch(choice)
            {
                case "1":
                    var balance = new Balance(_cuentaTarjeta);
                    await balance.ShowBalance();
                    break;
                case "2":
                    var retiro = new Retiro(_cuentaTarjeta);
                    await retiro.DisplayMessage();
                    break;
                case "3":
                    var reporte = new Reporte(_cuentaTarjeta);
                    await reporte.Display();
                    break;
                case "4":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            
            };
        }
    }

    public void Exit()
    {
        Environment.Exit(0);
    }
}