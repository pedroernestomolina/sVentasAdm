using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Cliente
{
    public class HndCliente: PanelPrincipal.Pago.ICliente
    {
        private OOB.CxC.CargarData.Cliente.Ficha _clientFicha;
        private string _client;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonar;
        private Utils.Control.Boton.Procesar.IProcesar _procesar;
        private bool _procesarIsok;
        private string _idCliente;
        private decimal _montoAnticipo;
        private decimal _montoAbonar;
        //
        public HndCliente()
        {
            _idCliente = "";
            _clientFicha = null;
            _client = "";
            _montoAnticipo = 0m;
            _montoAbonar = 0m;
            _procesarIsok = false;
            _abandonar = new Utils.Control.Boton.Abandonar.Imp();
            _procesar = new Utils.Control.Boton.Procesar.Imp();
        }

        public void Inicializa()
        {
            _client = "";
            _montoAnticipo = 0m;
            _montoAbonar = 0m;
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
        public void setFichaEntidadPagar(object ficha)
        {
            _clientFicha = (OOB.CxC.CargarData.Cliente.Ficha)ficha;
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
            _procesarIsok = false;
            if (_montoAbonar > _montoAnticipo) 
            {
                Helpers.Msg.Error("MONTO ANTICIPO A ABONAR INCORRECTO");
                return;
            }
            _procesar.Opcion();
            _procesarIsok = _procesar.OpcionIsOK;
        }


        //
        public object GetEntidadPagar { get { return _clientFicha; } }
        public string GetCliente { get { return _client; } }
        public decimal GetMontoAnticipo { get { return _montoAnticipo; } }
        public decimal GetMontoAbonar { get { return _montoAbonar; } }
        public void setMontoAbonar(decimal monto)
        {
            _montoAbonar = monto;
        }

        //
        private bool cargarData()
        {
            try
            {
                if (_idCliente == "") throw new Exception("CLIENTE [ ID ] NO SELECCIONADO");
                if (_clientFicha == null) throw new Exception("CLIENTE [ ENTIDAD ] NO SELECCIONADO");
                //var r01 = Sistema.MyData.CxC_CapturarData_Cliente_ById(_idCliente);
                //_clientFicha = r01.Entidad;
                _client = _clientFicha.ciRifClient + System.Environment.NewLine + _clientFicha.nombreRazonSocialClient + System.Environment.NewLine + _clientFicha.dirFiscalClient;
                _montoAnticipo = _clientFicha.montoAnticiposClient;
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