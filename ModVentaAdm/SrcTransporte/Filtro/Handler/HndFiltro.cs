using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Filtro.Handler
{
    public class HndFiltro: Vistas.IHndFiltro
    {
        private Utils.FiltroFecha.IFecha _desde;
        private Utils.FiltroFecha.IFecha _hasta;
        private Utils.FiltrosCB.ICtrlSinBusqueda _estatusDoc;
        private Utils.FiltrosCB.ICtrlConBusqueda _caja;
        private Utils.FiltrosCB.ICtrlConBusqueda _cliente;
        private Utils.FiltrosCB.ICtrlConBusqueda _aliado;


        public Utils.FiltrosCB.ICtrlConBusqueda Aliado { get { return _aliado; } }
        public Utils.FiltrosCB.ICtrlConBusqueda Cliente { get { return _cliente; } }
        public Utils.FiltrosCB.ICtrlSinBusqueda EstatusDoc { get { return _estatusDoc; } }
        public Utils.FiltroFecha.IFecha Desde { get { return _desde; } }
        public Utils.FiltroFecha.IFecha Hasta { get { return _hasta; } }


        public HndFiltro()
        {
            _desde = new Utils.FiltroFecha.Imp();
            _hasta = new Utils.FiltroFecha.Imp();
            _estatusDoc = new Utils.FiltrosCB.SinBusqueda.EstatusDoc.Imp();
            _caja = new Utils.FiltrosCB.ConBusqueda.Caja.Imp();
            _cliente = new Utils.FiltrosCB.ConBusqueda.Cliente.Imp();
            _aliado = new Utils.FiltrosCB.ConBusqueda.Aliado.Imp();
        }
        public void Inicializa()
        {
            _desde.Inicializa();
            _hasta.Inicializa();
            _estatusDoc.Inicializa();
            _caja.Inicializa();
            _cliente.Inicializa();
            _aliado.Inicializa();
        }
        public void CargarData()
        {
            _estatusDoc.ObtenerData();
            _caja.ObtenerData();
            _cliente.ObtenerData();
            _aliado.ObtenerData();
        }
        public void Limpiar()
        {
            _desde.Limpiar();
            _hasta.Limpiar();
            _estatusDoc.LimpiarOpcion();
            _caja.LimpiarOpcion();
            _cliente.LimpiarOpcion();
            _aliado.LimpiarOpcion();
        }


        //
        public DateTime Get_Desde { get { return _desde.Fecha; } }
        public DateTime Get_Hasta { get { return _hasta.Fecha; } }
        public bool Get_IsActivoDesde { get { return _desde.IsActiva; } }
        public bool Get_IsActivoHasta { get { return _hasta.IsActiva; } }
        public void setDesde(DateTime fecha)
        {
            _desde.setFecha(fecha);
        }
        public void setHasta(DateTime fecha)
        {
            _hasta.setFecha(fecha);
        }
        public void ActivarDesde(bool modo)
        {
            _desde.setActivar(modo);
        }
        public void ActivarHasta(bool modo)
        {
            _hasta.setActivar(modo);
        }

        //
        public BindingSource Get_EstatusSource { get { return _estatusDoc.GetSource; } }
        public string Get_EstatusById { get { return _estatusDoc.GetId; } }
        public void setEstatusById(string id)
        {
            _estatusDoc.setFichaById(id);
        }

        //
        public BindingSource Get_CajaSource { get { return _caja.GetSource; } }
        public string Get_CajaById { get { return _caja.GetId; } }
        public string GetCaja_TextoBuscar { get { return ""; } }
        public void setCajaById(string id)
        {
            _caja.setFichaById(id);
        }
        public void setCajaBuscar(string desc)
        {
            _caja.setTextoBuscar(desc);
        }

        //
        public BindingSource Get_ClienteSource { get { return _cliente.GetSource; } }
        public string Get_ClienteById { get { return _cliente.GetId; } }
        public string GetCliente_TextoBuscar{ get { return ""; } }
        public void setClienteById(string id)
        {
            _cliente.setFichaById(id);
        }
        public void setClienteBuscar(string desc)
        {
            _cliente.setTextoBuscar(desc);
        }


        //
        public Vistas.IdataFiltrar Get_Filtros
        {
            get { return retornarFiltros(); }
        }
        public bool VerificarFiltros()
        {
            if (_desde.IsActiva && _hasta.IsActiva)
            {
                if (_desde.Fecha > _hasta.Fecha)
                {
                    Helpers.Msg.Alerta("FECHAS INCORRECTAS");
                    return false;
                }
            }
            return true;
        }
        private Vistas.IdataFiltrar retornarFiltros()
        {
            var _filtroRet = new dataFiltrar(); ;
            if (_desde.IsActiva)
            {
                _filtroRet.Desde = _desde.Fecha;
            }
            if (_hasta.IsActiva)
            {
                _filtroRet.Hasta = _hasta.Fecha;
            }
            if (_estatusDoc.GetItem != null)
            {
                _filtroRet.EstatusDoc = _estatusDoc.GetId == "1" ? Vistas.Enumerados.EstatusDoc.Activo : Vistas.Enumerados.EstatusDoc.Anulado;
            }
            if (_caja.GetItem != null)
            {
                _filtroRet.IdCaja = int.Parse(_caja.GetId);
            }
            if (_cliente.GetItem != null)
            {
                _filtroRet.IdCliente = _cliente.GetId;
            }
            if (_aliado.GetItem != null)
            {
                var _aliadoItem = (Utils.FiltrosCB.Idata)_aliado.GetItem;
                var fichaOOB = (OOB.Transporte.Aliado.Entidad.Ficha)_aliadoItem.Ficha;
                _filtroRet.IdAliado = fichaOOB.id;
            }
            return _filtroRet;
        }
    }
}