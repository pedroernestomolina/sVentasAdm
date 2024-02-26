using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Filtro.Vista
{
    public interface IVista: Src.IGestion
    {
        Utils.Control.Boton.Salir.ISalir BtSalida{ get; }
        Utils.Control.Boton.Procesar.IProcesar BtAceptar { get; }
    }
}