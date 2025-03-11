using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Cliente
{
    public class HndCliente: PanelPrincipal.Pago.ICliente
    {
        private OOB.CxC.CargarData.Cliente.Ficha _client;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonar;
        private Utils.Control.Boton.Procesar.IProcesar _procesar;
        private bool _procesarIsok;
        private string _idCliente;
        //
        public HndCliente()
        {
            _idCliente = "";
            _client = null;
            _procesarIsok = false;
            _abandonar = new Utils.Control.Boton.Abandonar.Imp();
            _procesar = new Utils.Control.Boton.Procesar.Imp();
        }

        public void Inicializa()
        {
            _client = null;
            _procesarIsok = false;
            _abandonar.Inicializa();
            _procesar.Inicializa();
        }
        PanelPrincipal.Pago.vistas.vCliente frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                if (frm == null) 
                {
                    frm = new PanelPrincipal.Pago.vistas.vCliente();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setIdEntidad(object id)
        {
            _idCliente = (string)id;
        }
        //
        public bool AbandonarFichaIsOk { get { return _abandonar.OpcionIsOK; } }
        public void AbandonarFicha()
        {
            _abandonar.Opcion();
        }

        //
        public bool ProcesarFichaIsOk { get { return _procesarIsok; } }
        public void ProcesarFicha()
        {
            _procesar.Opcion();
            _procesarIsok = _procesar.OpcionIsOK;
        }

        //
        private bool cargarData()
        {
            try
            {
                if (_idCliente == "") throw new Exception("CLIENTE NO SELECCIONADO");
                var r01 = Sistema.MyData.CxC_CapturarData_Cliente_ById(_idCliente);
                _client = r01.Entidad;
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}