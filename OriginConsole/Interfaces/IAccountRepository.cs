using OriginConsole.Models;

namespace OriginConsole.Interfaces;

public interface IAccountRepository
{
    Task<CuentaTarjeta> Login(string pin);
}