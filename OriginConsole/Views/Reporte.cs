using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Repositories;
using OriginConsole.Services;
using OriginConsole.Utils;

namespace OriginConsole.Views;

public class Reporte: IView
{
    private IOperationService _operationService;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private Print _print;
    public Reporte(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _print = new Print(cuentaTarjeta);
        _operationService = new OperationService();
    }

    public async Task Display()
    {
        var reporte = await  _operationService.GetReporte(_cuentaTarjeta.Id);
        TablePrinter.Print(reporte);
        await _print.ShowBackOptions();
    } 
}
