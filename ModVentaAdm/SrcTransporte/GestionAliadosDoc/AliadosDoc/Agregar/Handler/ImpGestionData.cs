using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Agregar.Handler
{
    public class ImpGestionData: Vistas.IMainData
    {
        private Utils.FiltrosCB.ICtrlConBusqueda _aliado;
        private Utils.FiltrosCB.ICtrlConBusqueda _servicio;
        private decimal _monto;
        private string _servDesc;
        //
        public Utils.FiltrosCB.ICtrlConBusqueda Aliado { get { return _aliado; } }
        public Utils.FiltrosCB.ICtrlConBusqueda Servicio { get { return _servicio; } }
        //
        public ImpGestionData()
        {
            _aliado = new Utils.FiltrosCB.ConBusqueda.Aliado.Imp();
            _servicio= new Utils.FiltrosCB.ConBusqueda.Servicio.Imp();
            _monto = 0m;
            _servDesc = "";
        }
        public void Inicializa()
        {
            _aliado.Inicializa();
            _servicio.Inicializa();
            _monto = 0m;
            _servDesc = "";
        }
        public void setMonto(decimal monto)
        {
            _monto = monto;
        }
        public void setServicio(string desc)
        {
            _servDesc= desc;
        }
        public bool IsOk()
        {
            if (_aliado.GetItem == null) 
            {
                Helpers.Msg.Alerta("DEBES SELECCIONAR UN ALIADO");
                return false;
            }
            if (_servicio.GetItem == null)
            {
                Helpers.Msg.Alerta("DEBES SELECCIONAR UN SERVICIO");
                return false;
            }
            if (_monto <= 0m)
            {
                Helpers.Msg.Alerta("MONTO SERVICIO NO PUEDE SER CERO (0)");
                return false;
            }
            if (_servDesc.Trim()=="")
            {
                Helpers.Msg.Alerta("DESCRIPCION DEL SERVICIO NO PUEDE ESTAR VACIA");
                return false;
            }
            return true;
        }
        public object DataExportar()
        {
            return new dataExportar
            {
                montoDiv=_monto,
                servDesc= _servDesc,
                aliado=_aliado.GetItem,
                servicio=_servicio.GetItem,
            };
        }
    }
}