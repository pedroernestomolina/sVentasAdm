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
        //
        public decimal GetMontoPagar { get { return 0m; } }
        public HndPanelCliente()
        {
            _idCliente = "";
            _fechaServ = DateTime.Now.Date;
            _hndCliente = new HndCliente();
        }
        public void Inicializa()
        {
            _idCliente = "";
            _fechaServ = DateTime.Now.Date;
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
            _hndCliente.Inicia();
        }
    }
}