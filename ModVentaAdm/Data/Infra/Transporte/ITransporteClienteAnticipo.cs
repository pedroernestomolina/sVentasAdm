using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteClienteAnticipo
    {
        OOB.Resultado.FichaEntidad<int>
            Transporte_Cliente_Anticipo_Agregar(OOB.Transporte.ClienteAnticipo.Agregar.Ficha ficha);
    }
}