using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Agregar.Handler
{
    public class ImpMain: Vistas.IMain
    {
        private Utils.Control.Boton.Abandonar.IAbandonar _btAbandonar;
        private Utils.Control.Boton.Procesar.IProcesar _btProcesar;
        private Vistas.IMainData _gestionData;
        private bool _procesarIsOk;
        //
        public Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get { return _btAbandonar; } }
        public Utils.Control.Boton.Procesar.IProcesar BtProcesar { get { return _btProcesar; } }
        public Vistas.IMainData GestionData { get { return _gestionData; } }
        public bool ProcesarIsOk { get { return _procesarIsOk; } }
        //
        public ImpMain()
        {
            _btAbandonar = new Utils.Control.Boton.Abandonar.Imp();
            _btProcesar = new Utils.Control.Boton.Procesar.Imp();
            _gestionData = new ImpGestionData();
            _procesarIsOk = false;
        }
        public void Inicializa()
        {
            _btAbandonar.Inicializa();
            _btProcesar.Inicializa();
            _gestionData.Inicializa();
            _procesarIsOk = false;
        }
        Vistas.Frm frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                if (frm == null) 
                {
                    frm = new Vistas.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public object dataAgregar()
        {
            return _gestionData.DataExportar();
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_gestionData.IsOk()) 
            {
                _btProcesar.Opcion();
                if (_btProcesar.OpcionIsOK) 
                {
                    _procesarIsOk = true;
                }
            }
        }
        //
        private bool cargarData()
        {
            _gestionData.Aliado.ObtenerData();
            _gestionData.Servicio.ObtenerData();
            return true;
        }
    }
}