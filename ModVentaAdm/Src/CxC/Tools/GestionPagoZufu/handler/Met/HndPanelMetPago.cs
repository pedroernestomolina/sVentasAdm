using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public class HndPanelMetPago: Tools.PanelPrincipal.Pago.IPanelMetPago
    {
        private decimal _factCambio;
        private PanelPrincipal.Pago.IMetodoPagoGestionAgregar _agregarMet;
        private PanelPrincipal.Pago.IListaMetPago _listMetPago;
        //
        public IEnumerable<PanelPrincipal.Pago.IItemMetPago> GetListaMetPago { get { return _listMetPago.GetListaMetPago; } }
        public decimal GetMontoRecibido { get { return _listMetPago.GetMontoRecibido; } }
        public string GetCntMetRecibido { get { return _listMetPago.GetCntMetRecibido; } }
        //
        public HndPanelMetPago() 
        {
            _factCambio = 0m;
            _listMetPago = new HndListMetPago();
        }
        public void Inicializa()
        {
            _factCambio = 0m;
            _listMetPago.Inicializa();
        }
        public void AgregarMetPago()
        {
            if (_agregarMet == null) 
            {
                _agregarMet = new HndMetPagoGestionAgregar();
            }
            _agregarMet.Inicializa();
            _agregarMet.setFactor(_factCambio);
            _agregarMet.Inicia();
            if (_agregarMet.ProcesarIsOK) 
            {
               _listMetPago.AgregarMet(_agregarMet.ItemAgregar);
            }
        }
        public void ListarMetPago()
        {
            _listMetPago.Inicia();
        }
        public void setFactorDivisa(decimal fact)
        {
            _factCambio = fact;
        }
    }
}