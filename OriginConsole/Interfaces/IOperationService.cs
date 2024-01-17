using OriginConsole.Models;

namespace OriginConsole.Interfaces;

public interface IOperationService
{
    Task RegisterWithdraw(int id, decimal amount, decimal previousAmount);
    Task<IEnumerable<ReporteDto>> GetReporte(int id);
    Task RegisterBalance(int id, decimal amount);
}