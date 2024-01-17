using OriginConsole.Models;
using OriginConsole.Models.Dto;

namespace OriginConsole.Interfaces;

public interface ICardRepository
{
    Task<Tarjeta>? FindCard(string number);
    Task UpdateCard(Tarjeta newCard);
    Task<BalanceDto> GetBalance(int id);
}