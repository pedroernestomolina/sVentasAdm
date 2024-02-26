using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Filtro.Handler
{
    public abstract class baseImp: Vista.IVista
    {
        private Control.Boton.Salir.ISalir _btSalida;
        private Control.Boton.Procesar.IProcesar _btAceptar;
        //
        public Control.Boton.Salir.ISalir BtSalida { get { return _btSalida; } }
        public Control.Boton.Procesar.IProcesar BtAceptar { get { return _btAceptar; } }
        public baseImp()
        {
            _btSalida = new Control.Boton.Salir.Imp();
            _btAceptar = new Control.Boton.Procesar.Imp();
        }
        public abstract void Inicializa();
        public abstract void Inicia();
    }
}