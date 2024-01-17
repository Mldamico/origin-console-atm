using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Repositories;

namespace OriginConsole.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService()
    {
        _cardRepository = new CardRepository();
    }

    public async Task<BalanceDto> GetBalance(int id)
    {
        return await _cardRepository.GetBalance(id);
    }
    
    public async Task UpdateCard(Tarjeta tarjeta)
    {
        
        
        await _cardRepository.UpdateCard(tarjeta);
    }
}