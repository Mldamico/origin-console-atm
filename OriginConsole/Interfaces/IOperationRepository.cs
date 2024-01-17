using OriginConsole.Models;
using OriginConsole.Models.Dto;

namespace OriginConsole.Interfaces;

public interface IOperationRepository
{
    Task RegisterBalance(int id, decimal previousBalance);
    Task RegisterWithdraw(int id, decimal amount, decimal previousAmount);
    Task<IEnumerable<ReporteDto>> GetReporte(int id);
    
    
}