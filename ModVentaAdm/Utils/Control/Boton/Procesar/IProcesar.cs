using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Control.Boton.Procesar
{
    public interface IProcesar: IBoton
    {
        void setOpcion(bool p);
    }
}