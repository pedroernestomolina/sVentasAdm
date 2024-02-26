using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Control.Boton
{
    public interface IBoton
    {
        bool OpcionIsOK { get; }
        void Inicializa();
        void Opcion();
    }
}