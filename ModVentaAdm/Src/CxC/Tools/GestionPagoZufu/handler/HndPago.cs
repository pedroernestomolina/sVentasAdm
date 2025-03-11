using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    public class HndPago: PanelPrincipal.Pago.IPago
    {
        private string _idClientePagar;
        private DateTime _fechaServidor;
        private decimal _factorDivActual;
        private bool _pagoExitoso;
        private Utils.Control.Boton.Abandonar.IAbandonar _abandonar;
        private Utils.Control.Boton.Procesar.IProcesar _procesar;
        private PanelPrincipal.Pago.IPanelMetPago _panMetPago;
        private PanelPrincipal.Pago.IPanelCtas _panCtas;
        private PanelPrincipal.Pago.IPanelCtas _panNtCred;
        private PanelPrincipal.Pago.IPanelResumen _panResumen;
        private PanelPrincipal.Pago.IPanelCliente _panCliente;
        //
        public HndPago() 
        {
            _abandonar = new Utils.Control.Boton.Abandonar.Imp();
            _procesar = new Utils.Control.Boton.Procesar.Imp();
            _panMetPago = new Met.HndPanelMetPago();
            _panCtas= new Ctas.HndPanelCtas();
            _panNtCred = new NtCred.HndPanelNtCred();
            _panResumen = new Resumen.HndPanelResumen();
            _panCliente = new Cliente.HndPanelCliente();
        }
        public void Inicializa()
        {
            _pagoExitoso = false;
            _fechaServidor = DateTime.Now.Date;
            _factorDivActual = 0m;
            _abandonar.Inicializa();
            _procesar.Inicializa();
            _panMetPago.Inicializa();
            _panCtas.Inicializa();
            _panNtCred.Inicializa();
            _panResumen.Inicializa();
            _panCliente.Inicializa();
        }
        private PanelPrincipal.Pago.vistas.vPago frm;
        public void Inicia()
        {
            if (cargarData())
            {
                if (frm == null)
                {
                    frm = new PanelPrincipal.Pago.vistas.vPago();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setIdEntidadPagar(object id)
        {
            _idClientePagar = (string)id;
            _panCtas.setIdEntidad(id);
            _panNtCred.setIdEntidad(id);
            _panCliente.setIdEntidad(id);
        }
        //
        private bool cargarData()
        {
            try
            {
                var r01 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                setFactorDivisa(r01.Entidad);
                var r02 = Sistema.MyData.FechaServidor();
                if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r02.Mensaje);
                }
                setFechaServidor(r02.Entidad);
                //
                _panMetPago.setFactorDivisa(r01.Entidad);
                _panCtas.setFechaServidor(r02.Entidad);
                _panNtCred.setFechaServidor(r02.Entidad);
                _panCliente.setFechaServidor(r02.Entidad);
                //
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private void setFechaServidor(DateTime fech)
        {
            _fechaServidor = fech;
        }
        private void setFactorDivisa(decimal factor)
        {
            _factorDivActual = factor;
        }
        //
        public bool AbandonarFichaIsOk { get { return _abandonar.OpcionIsOK; } }
        public void AbandonarFicha()
        {
            _abandonar.Opcion();
        }
        //
        public bool IsPagoExitoso { get { return _pagoExitoso; } }
        public void ProcesarPago()
        {
            _pagoExitoso = false;
            _procesar.Opcion();
            if (_procesar.OpcionIsOK) 
            {
                guardarPago();
            }
        }
        //
        private void guardarPago()
        {
            try
            {
                var fichaOOB = (OOB.CxC.GestionCobro.Ficha)FichaCobro();
                var rt1 = Sistema.MyData.CxC_GestionCobro_Agregar(fichaOOB);
                if (rt1.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(rt1.Mensaje);
                }
                _pagoExitoso = true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
            return;
        }
        //
        private object FichaCobro()
        {
            var oob = new OOB.CxC.GestionCobro.Ficha()
            {
                 MetodosPago = null,
            };
            return oob;
        }

        //PANEL: MET
        public string GetCntMetRecibido { get { return _panMetPago.GetCntMetRecibido; } }
        public decimal GetMontoRecibido { get { return _panMetPago.GetMontoRecibido; } }
        public void AgregarMetPago()
        {
            _panMetPago.AgregarMetPago();
            _panResumen.setMontoMetPago(_panMetPago.GetMontoRecibido);
        }
        public void ListarMetPago()
        {
            _panMetPago.ListarMetPago();
            _panResumen.setMontoMetPago(_panMetPago.GetMontoRecibido);
        }

        //PANEL: CTAS
        public string GetCntCtasPagar { get { return _panCtas.GetCntCtasPagar; } }
        public decimal GetMontoCtasPagar { get { return _panCtas.GetMontoPagar; } }
        public void ListarCtasPagar()
        {
            _panCtas.ListarCtasPagar();
            _panResumen.setMontoCtasPend(_panCtas.GetMontoPagar);
        }

        //PANEL: NOTAS_CREDITO
        public string GetCntNtCred { get { return _panNtCred.GetCntCtasPagar; } }
        public decimal GetMontoNtCred { get { return _panNtCred.GetMontoPagar; } }
        public void ListarNtCred()
        {
            _panNtCred.ListarCtasPagar();
            _panResumen.setMontoNtCredito(_panNtCred.GetMontoPagar);
        }

        //PANEL: RESUMEN
        public decimal GetResumenMontoAnticipo { get { return _panResumen.GetResumenMontoAnticipo; } }
        public decimal GetResumenMontoMetPago { get { return _panResumen.GetResumenMontoMetPago; } }
        public decimal GetResumenMontoNtCredito { get { return _panResumen.GetResumenMontoNtCredito; } }
        public decimal GetResumenMontoAbono { get { return _panResumen.GetResumenMontoAbono; } }
        public decimal GetResumenMontoCtasPend { get { return _panResumen.GetResumenMontoCtasPend; } }
        public decimal GetResumenSaldo { get { return _panResumen.GetResumenSaldo; } }
        public string GetResumenSaldoDesc { get { return _panResumen.GetResumenSaldoDesc ; } }

        //PANEL: ANTICIPO
        public decimal GetMontoAnticipo { get { return _panCliente.GetMontoPagar; } }
        public void AgregarAnticipo()        
        {
            _panCliente.AgregarAnticipo();
            _panResumen.setMontoAnticipo(_panCliente.GetMontoPagar);
        }
    }
}