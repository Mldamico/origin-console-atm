using OriginConsole.Data;
using OriginConsole.Interfaces;
using OriginConsole.Servicios;

namespace OriginConsole.Views;

public static class Init
{
   
    public static async Task Execute()
    {
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
        
    }
    
}