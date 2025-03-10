using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public class listaMetPago
    {
        private BindingList<PanelPrincipal.Pago.IItemMetPago> _bl;
        private BindingSource _bs;
        //
        public BindingSource Source { get { return _bs; } }
        public itemMetPago ItemActual { get { return (itemMetPago)_bs.Current; } }
        public int GetCntMetRecibido { get { return _bs.Count; } } 
        public decimal GetMontoRecibido { get { return Math.Round(_bl.Sum(s => s.ImporteMonDiv), 2, MidpointRounding.AwayFromZero); } }
        public string GetMetodoPagoOp { get { return ItemActual != null ? ItemActual.MetCobro.desc: ""; } }
        public decimal GetMontoOp { get { return ItemActual != null ? ItemActual.Monto : 0m; } }
        public DateTime GetFechaOp { get { return ItemActual != null ? ItemActual.FechaOp: DateTime.Now.Date; } }
        public string GetDetalleOp { get { return ItemActual != null ? ItemActual.DetalleOp : ""; } }
        public string GetNroCtaOp { get { return ItemActual != null ? ItemActual.NroCta: ""; } }
        public string GetRefOp { get { return ItemActual != null ? ItemActual.CheqRefTranf: ""; } }
        public string GetBancoOp { get { return ItemActual != null ? ItemActual.Banco: ""; } }
        public string GetAplicaFactorOp
        {
            get
            {
                var rt = "";
                if ( ItemActual!= null)
                {
                    if (ItemActual.AplicaFactor)
                    {
                        rt += "Si Aplica, " + Environment.NewLine + "Tasa: " + ItemActual.FactorCambio.ToString("n2");
                    }
                    else
                    {
                        rt += "No Aplica," + Environment.NewLine + "Monto en ($)";
                    }
                }
                return rt;
            }
        }
        //
        public listaMetPago()
        {
            _bl = new BindingList<PanelPrincipal.Pago.IItemMetPago>();
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }
        public void Inicializa()
        {
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void AgregarMet(PanelPrincipal.Pago.IItemMetPago itemMetPago)
        {
            _bl.Add(itemMetPago);
        }
        public void EliminarItemActual()
        {
            if (ItemActual != null)
            {
                _bl.Remove(ItemActual);
                _bs.CurrencyManager.Refresh();
            }
        }
    }
}