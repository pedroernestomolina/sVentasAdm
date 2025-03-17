using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Cliente
{
    public class HndPanelCliente: PanelPrincipal.Pago.IPanelCliente
    {
        private string _idCliente;
        private DateTime _fechaServ;
        private PanelPrincipal.Pago.ICliente _hndCliente;
        private decimal _montoAbonar;
        private decimal _montoAnticipoDisponible;
        //
        public object GetEntidadPagar { get { return _hndCliente.GetEntidadPagar; } }
        public decimal GetMontoPagar { get { return _montoAbonar; } }
        public decimal GetMontoAnticipoDisponible { get { return _montoAnticipoDisponible; } }
        //
        public HndPanelCliente()
        {
            _montoAbonar = 0m;
            _idCliente = "";
            _montoAbonar = 0m;
            _fechaServ = DateTime.Now.Date;
            _hndCliente = new HndCliente();
            _montoAnticipoDisponible = 0m;
        }
        public void Inicializa()
        {
            _idCliente = "";
            _montoAbonar = 0m;
            _fechaServ = DateTime.Now.Date;
            _montoAnticipoDisponible = 0m;
        }
        public void setIdEntidad(object id)
        {
            _idCliente = (string)id;
            _hndCliente.setIdEntidad(id);
        }
        public void setFechaServidor(DateTime fecha)
        {
            _fechaServ = fecha;
        }
        public void AgregarAnticipo()
        {
            _hndCliente.Inicializa();
            _hndCliente.setMontoAbonar(_montoAbonar);
            _hndCliente.Inicia();
            if (_hndCliente.ProcesarFichaIsOk) 
            {
                _montoAbonar = _hndCliente.GetMontoAbonar;
            }
        }
        public void setFichaEntidadPagar(object ficha)
        {
            _hndCliente.setFichaEntidadPagar(ficha);
        }
        public void setMontoAnticiposDisponible(decimal monto)
        {
            _montoAnticipoDisponible = monto;
        }
    }
}