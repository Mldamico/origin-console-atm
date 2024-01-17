using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Repositories;

namespace OriginConsole.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService()
    {
        _accountRepository = new AccountRepository();
    }

    public async Task<CuentaTarjeta> Login(string pin)
    {
        var cuenta = await  _accountRepository.Login(pin);
        if (cuenta is null)
        {
            return null;
        }
        if (cuenta.Bloqueada == 1)
        {
            Console.WriteLine("Cuenta bloqueada");
            Environment.Exit(0);
        }
        
        Console.WriteLine(cuenta.intentos_restantes);

        

        return cuenta;
    }
}