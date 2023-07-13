using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Remision
{
    public interface IObservador
    {
        void NotificarRemisionDocPresupuesto(OOB.Transporte.Documento.Entidad.Presupuesto.Ficha ficha);
    }
}