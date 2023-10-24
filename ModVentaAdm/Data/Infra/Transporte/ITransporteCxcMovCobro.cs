using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteCxcMovCobro
    {
        OOB.Resultado.Lista<OOB.Transporte.CxcMovCobro.ListaMov.Ficha>
            Transporte_CxcMovCobro_GetLista(OOB.Transporte.CxcMovCobro.ListaMov.Filtro filtro);
    }
}