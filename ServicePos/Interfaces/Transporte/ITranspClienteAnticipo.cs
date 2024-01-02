using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITranspClienteAnticipo
    {
        DtoLib.ResultadoId
            Transporte_Cliente_Anticipo_Agregar(DtoTransporte.ClienteAnticipo.Agregar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoTransporte.ClienteAnticipo.Obtener.Ficha>
            Transporte_Cliente_Anticipo_Obtener_ById(string idCliente);
        DtoLib.ResultadoLista<DtoTransporte.ClienteAnticipo.ListaMov.Ficha>
            Transporte_Cliente_Anticipo_GetLista(DtoTransporte.ClienteAnticipo.ListaMov.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoTransporte.ClienteAnticipo.Entidad.Ficha>
            Transporte_Cliente_Anticipo_Movimiento_GetById(int idMov);
        //
        DtoLib.ResultadoEntidad<DtoTransporte.ClienteAnticipo.Anular.Ficha>
            Transporte_Cliente_Anticipo_Anular_ObtenerData(int idMov);
        DtoLib.Resultado
            Transporte_Cliente_Anticipo_Anular(DtoTransporte.ClienteAnticipo.Anular.Ficha ficha);
    }
}
