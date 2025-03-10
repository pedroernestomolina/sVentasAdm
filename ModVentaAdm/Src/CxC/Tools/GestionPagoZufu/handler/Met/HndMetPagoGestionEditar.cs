using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public class HndMetPagoGestionEditar : baseMetPagoGestionAgregarEditar, PanelPrincipal.Pago.IMetodoPagoGestionEditar
    {
        private PanelPrincipal.Pago.IItemMetPago _itemEditar;
        //
        public PanelPrincipal.Pago.IItemMetPago ItemActualizado { get { return nuevoItem(); } }
        public override string GetTituloFicha { get { return "Editar Metodo De Pago"; } }
        //
        public HndMetPagoGestionEditar(): base()
        {
            _itemEditar = null;
        }
        public override void Inicializa()
        {
            base.Inicializa();
            _itemEditar = null;
        }
        PanelPrincipal.Pago.vistas.vMetPago frm;
        public override void Inicia()
        {
            if (CargarDta()) 
            {
                if (_itemEditar != null) 
                {
                    setMonto(_itemEditar.Monto);
                    setFactor(_itemEditar.FactorCambio);
                    setBanco(_itemEditar.Banco);
                    setCtaNro(_itemEditar.NroCta);
                    setChequeRefTranf(_itemEditar.CheqRefTranf);
                    setFechaOperacion(_itemEditar.FechaOp);
                    setDetalleOperacion(_itemEditar.DetalleOp);
                    setAplicaFactor(_itemEditar.AplicaFactor);
                    setLote(_itemEditar.Lote);
                    setMetCobro(_itemEditar.MetCobro.id);
                    setReferencia(_itemEditar.Referencia);
                }
                if (frm == null) 
                {
                    frm = new PanelPrincipal.Pago.vistas.vMetPago();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setItemEditar(PanelPrincipal.Pago.IItemMetPago item)
        {
            _itemEditar = item;
        }
    }
}