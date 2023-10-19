using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.CajasUtilizar.Vista
{
    public interface Idata
    {
        string descripcion { get; set; }
        decimal saldoActual { get; set; }
        decimal montoAbonar { get; set; }
        bool esDivisa { get; set; }
        void setMontoAbonar(decimal monto);
    }
}