using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.Pendiente
{
    
    public class data
    {

        private OOB.Venta.Temporal.Pendiente.Lista.Ficha _rg;


        public int Id { get { return _rg.id; } }
        public string FechaHora { get { return _rg.fecha.ToShortDateString() + ", " + _rg.hora; } }
        public string Cliente { get { return _rg.ciRifCliente + ", "+ _rg.nombreCliente; } }
        public int Renglones { get { return _rg.renglones ; } }
        public decimal Monto { get { return _rg.monto; } }
        public decimal MontoDivisa { get { return _rg.montoDivisa; } }
        public string Deposito { get { return _rg.depositoNombre; } }


        public data(OOB.Venta.Temporal.Pendiente.Lista.Ficha rg)
        {
            this._rg = rg;
        }

    }

}