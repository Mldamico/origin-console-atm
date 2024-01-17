using OriginConsole.Models;
using OriginConsole.Models.Dto;

namespace OriginConsole.Interfaces;

public interface IOperationRepository
{
    Task RegisterBalance(int id);
    Task RegisterWithdraw(int id, decimal monto);
    Task<IEnumerable<ReporteDto>> GetReporte(int id);
    
    
}