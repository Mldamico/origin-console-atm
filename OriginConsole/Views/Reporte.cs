using OriginConsole.Interfaces;
using OriginConsole.Models;
using OriginConsole.Servicios;
using OriginConsole.Utils;

namespace OriginConsole.Views;

public class Reporte
{
    private IOperationRepository _operationRepository;
    private readonly CuentaTarjeta _cuentaTarjeta;
    private Print _print;
    public Reporte(CuentaTarjeta cuentaTarjeta)
    {
        _cuentaTarjeta = cuentaTarjeta;
        _print = new Print(cuentaTarjeta);
        _operationRepository = new OperationRepository();
    }

    public async Task Display()
    {
        var reporte =await  _operationRepository.GetReporte(_cuentaTarjeta.Id);
        TablePrinter.Print(reporte);
        await _print.ShowBackOptions();
    } 
}
