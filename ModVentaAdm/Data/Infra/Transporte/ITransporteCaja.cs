using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteCaja
    {
        OOB.Resultado.Lista<OOB.Transporte.Caja.Lista.Ficha>
            Transporte_Caja_GetLista();
    }
}