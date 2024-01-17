using OriginConsole.Data;
using OriginConsole.Interfaces;
using OriginConsole.Servicios;

namespace OriginConsole.Home;

public class Init
{
    private readonly ICardRepository _cardRepository;

    public Init(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public  async Task Execute()
    {
        var menu = new Menu(_cardRepository);
        
        
        
        var seed = new SeedData();
        
        try
        {
            await seed.CreateDB();
            await seed.Initialize();
            await seed.Seed();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        await menu.ShowMenu();
    }
    
}