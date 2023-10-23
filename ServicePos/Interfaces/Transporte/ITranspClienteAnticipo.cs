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
    }
}
