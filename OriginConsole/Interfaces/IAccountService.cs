using OriginConsole.Models;

namespace OriginConsole.Interfaces;

public interface IAccountService
{
    Task<CuentaTarjeta> Login(string pin);
}