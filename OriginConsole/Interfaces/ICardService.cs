using OriginConsole.Models;

namespace OriginConsole.Interfaces;

public interface ICardService
{
    Task UpdateCard(Tarjeta tarjeta);
    Task<BalanceDto> GetBalance(int id);
}