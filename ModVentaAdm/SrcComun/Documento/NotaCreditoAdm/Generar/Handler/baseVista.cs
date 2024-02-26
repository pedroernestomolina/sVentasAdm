using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Documento.NotaCreditoAdm.Generar.Handler
{
    abstract public class baseVista: Vista.IVista
    {
        private Utils.Control.Boton.Abandonar.IAbandonar _btAbandonar;
        private Utils.Control.Boton.Procesar.IProcesar _btProcesar;
        //
        public Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get { return _btAbandonar; } }
        public Utils.Control.Boton.Procesar.IProcesar BtProcesar { get { return _btProcesar; } }
        //
        public baseVista()
        {
            _btAbandonar = new Utils.Control.Boton.Abandonar.Imp();
            _btProcesar = new Utils.Control.Boton.Procesar.Imp();
        }
        virtual public void Inicializa()
        {
            _btAbandonar.Inicializa();
            _btProcesar.Inicializa();
        }
        abstract public void Inicia();
    }
}