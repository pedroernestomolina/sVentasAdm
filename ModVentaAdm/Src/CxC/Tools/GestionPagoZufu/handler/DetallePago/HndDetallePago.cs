using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.DetallePago
{
    public class HndDetallePago: PanelPrincipal.Pago.IDetallePago
    {
        private data _data;
        private bool _procesarIsOk;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonar;
        private Utils.Control.Boton.Procesar.IProcesar _procesar;
        private Utils.FiltrosCB.ICtrlSinBusqueda _gCobrador;
        private DateTime _fechaProceso;
        private string _notas;
        private LibUtilitis.Opcion.IData _cobrador;
        //
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public bool AbandonarIsOK { get { return _abandonar.OpcionIsOK; } }
        //
        public bool DetalleIsOk { get { return _procesarIsOk; } }
        public string GetNotas { get { return _notas; } }
        public string GetIdCobrador { get { return _gCobrador.GetId; } }
        public Object CobradorSource { get { return _gCobrador.GetSource; } }
        public DateTime GetFechaProceso { get { return _fechaProceso; } }
        public object GetCobrador { get { return _cobrador; } }
        //
        public HndDetallePago() 
        {
            _procesarIsOk = false;
            _abandonar = new Utils.Control.Boton.Abandonar.Imp();
            _procesar = new Utils.Control.Boton.Procesar.Imp();
            _gCobrador = new Utils.FiltrosCB.SinBusqueda.Cobradores.Imp();
            _data = new data();
            _fechaProceso = DateTime.Now.Date;
            _notas = "";
            _cobrador = null;
        }
        public void Inicializa()
        {
            _procesarIsOk = false;
            _data.Inicializa();
            _abandonar.Inicializa();
            _procesar.Inicializa();
        }
        PanelPrincipal.Pago.vistas.vDetallePago frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                setNotas(_notas);
                setFechaProceso(_fechaProceso);
                setCobrador(_cobrador==null?"":_cobrador.id);
                if (frm == null)
                {
                    frm = new PanelPrincipal.Pago.vistas.vDetallePago();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void AbandonarFicha()
        {
            _abandonar.Opcion();
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_data.IsOk()) 
            {
                _procesar.Opcion();
                _procesarIsOk = _procesar.OpcionIsOK;
                if (_procesarIsOk) 
                {
                    _fechaProceso = _data.GetFechaProceso;
                    _notas = _data.GetNotas;
                    _cobrador = _data.GetCobrador;
                }
            }
        }
        public void setNotas(string p)
        {
            _data.setNotas(p);
        }
        public void setCobrador(string id)
        {
            _gCobrador.setFichaById(id);
            var _item = _gCobrador.GetItem;
            _data.setCobrador(_item);
        }
        public void setFechaProceso(DateTime fechaProceso)
        {
            _data.setFechaProceso(fechaProceso);
        }
        //
        private bool cargarData()
        {
            try
            {
                _gCobrador.ObtenerData();
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        //
        public void LimpiarData()
        {
            _fechaProceso = DateTime.Now.Date;
            _notas = "";
            _cobrador = null;
        }
    }
}