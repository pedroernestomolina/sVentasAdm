using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Agregar.Vistas
{
    public interface IdataCaja
    {
        string descripcion { get; set; }
        decimal saldoActual { get; set; }
        decimal montoAbonar { get; set; }
        bool esDivisa { get; set; }
        void setMontoAbonar(decimal monto);
    }
}