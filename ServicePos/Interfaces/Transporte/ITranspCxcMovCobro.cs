using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces.Transporte
{
    public interface ITranspCxcMovCobro
    {
        DtoLib.ResultadoLista<DtoTransporte.CxcMovCobro.ListaMov.Ficha>
            Transporte_CxcMovCobro_GetLista(DtoTransporte.CxcMovCobro.ListaMov.Filtro filtro);
    }
}
