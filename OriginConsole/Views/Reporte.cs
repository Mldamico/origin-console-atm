using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Servicios;

namespace OriginConsole.Home;

public class Reporte
{
    private IOperationRepository _operationRepository;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private ICardRepository _cardRepository;
    public Reporte(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _cardRepository = new CardRepository();
        _operationRepository = new OperationRepository();
    }

    public async Task Display()
    {
        var reporte =await  _operationRepository.GetReporte(_cuentaTarjeta.Id);
        TablePrinter.Print(reporte);
        Console.WriteLine("Que desea hacer?");
        Console.WriteLine("1 - Atras");
        Console.WriteLine("2 - Salir");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                var home = new Home(_cuentaTarjeta);
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

public static class TablePrinter
{
    public static void Print<T>(IEnumerable<T> items)
    {
        const int columnWidth = 20;
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)    
        {
            Console.Write($"{{0,-{columnWidth}}}|", property.Name);
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', properties.Length * (columnWidth+1)));
        
        foreach (var item in items)
        {
            foreach (var property in properties)
            {
                Console.Write($"{{0,-{columnWidth}}}|", property.GetValue(item));
            }
            Console.WriteLine();
        }
    }
}