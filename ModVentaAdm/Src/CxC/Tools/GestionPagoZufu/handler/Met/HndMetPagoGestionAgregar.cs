using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.Met
{
    public class HndMetPagoGestionAgregar: baseMetPagoGestionAgregarEditar, PanelPrincipal.Pago.IMetodoPagoGestionAgregar
    {
        public override string GetTituloFicha { get { return "Agregar Metodo De Pago"; } }
        //
        public PanelPrincipal.Pago.IItemMetPago ItemAgregar { get { return nuevoItem(); } }
        //
        public HndMetPagoGestionAgregar(): base()
        {
        }
        public void Inicializa()
        {
            base.Inicializa();
        }
        PanelPrincipal.Pago.vistas.vMetPago frm;
        public override void Inicia()
        {
            if (CargarDta()) 
            {
                if (frm == null) 
                {
                    frm = new PanelPrincipal.Pago.vistas.vMetPago();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
    }
}