using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.DatosDocumento
{
    public interface IDatosDoc: Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar
    {
        data Data { get; }

        void BuscarCliente();
        void setEscucha(DocVenta.Generar.dataItem items);
    }
}