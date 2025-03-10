using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public class HndListMetPago: PanelPrincipal.Pago.IListaMetPago
    {
        private listaMetPago _listMetPag;
        private PanelPrincipal.Pago.IMetodoPagoGestionEditar _editarMetPag;
        //
        public object Source { get { return _listMetPag.Source; } }
        public itemMetPago ItemActual { get { return _listMetPag.ItemActual; } }
        public string GetCntMetRecibido { get { return strToInt(_listMetPag.GetCntMetRecibido) ; } }
        public string GetMontoRecibido { get { return strToDec(_listMetPag.GetMontoRecibido); } }
        public string GetMetodoPagoOp { get { return _listMetPag.GetMetodoPagoOp; } }
        public decimal GetMontoOp { get { return _listMetPag.GetMontoOp; } }
        public DateTime GetFechaOp { get { return _listMetPag.GetFechaOp; } }
        public string GetDetalleOp { get { return _listMetPag.GetDetalleOp; } }
        public string GetNroCtaOp { get { return _listMetPag.GetNroCtaOp; } }
        public string GetRefOp { get { return _listMetPag.GetRefOp; } }
        public string GetBancoOp { get { return _listMetPag.GetBancoOp; } }
        public string GetAplicaFactorOp { get { return _listMetPag.GetAplicaFactorOp; } }
        //
        public HndListMetPago()
        {
            _listMetPag = new listaMetPago();
            _editarMetPag = new HndMetPagoGestionEditar();
        }
        public void Inicializa()
        {
            _listMetPag.Inicializa();
        }
        PanelPrincipal.Pago.vistas.vMetPagoLista frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                if (frm == null) 
                {
                    frm = new PanelPrincipal.Pago.vistas.vMetPagoLista();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void AgregarMet(PanelPrincipal.Pago.IItemMetPago itemMetPago)
        {
            _listMetPag.AgregarMet(itemMetPago);
        }
        public void EliminarMetodoPago()
        {
            if (ItemActual != null)
            {
                var prc = Helpers.Msg.ProcesarGuardar("Eliminar Metodo De Pago ?");
                if (prc)
                {
                    _listMetPag.EliminarItemActual();
                }
            }
        }
        public void EditarMetodoPago()
        {
            if (ItemActual != null)
            {
                _editarMetPag.Inicializa();
                _editarMetPag.setItemEditar(ItemActual);
                _editarMetPag.Inicia();
                if (_editarMetPag.ProcesarIsOK) 
                {
                    _listMetPag.EliminarItemActual();
                    _listMetPag.AgregarMet(_editarMetPag.ItemActualizado);
                }
            }
        }
        //
        private bool cargarData()
        {
            return true;
        }
        private string strToDec(decimal mont)
        {
            return mont.ToString("n2");
        }
        private string strToInt(int cnt)
        {
            return cnt.ToString();
        }
    }
}