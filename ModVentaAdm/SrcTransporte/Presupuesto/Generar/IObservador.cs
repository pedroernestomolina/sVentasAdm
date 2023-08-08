using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public interface IObservador
    {
        void NotificarDocPresupuesto(OOB.Transporte.Documento.Entidad.Presupuesto.Ficha ficha);
    }
}