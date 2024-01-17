using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Repositories;

namespace OriginConsole.Services;

public class OperationService: IOperationService
{
    private readonly IOperationRepository _operationRepository;

    public OperationService()
    {
        _operationRepository = new OperationRepository();
    }

    public async Task RegisterWithdraw(int id,decimal amount, decimal previousAmount)
    {
        await _operationRepository.RegisterWithdraw(id, amount, previousAmount);
    }

    public async Task<IEnumerable<ReporteDto>> GetReporte(int id)
    {
        return await _operationRepository.GetReporte(id);
    }

    public async Task RegisterBalance(int id, decimal amount)
    {
        await _operationRepository.RegisterBalance(id, amount);
    }
    
}