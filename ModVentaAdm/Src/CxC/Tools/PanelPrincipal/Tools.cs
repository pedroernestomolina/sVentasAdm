using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal
{
    
    public class Tools: ITools
    {


        private ListaCtasPend.ILista _gListaCtasPend;


        public decimal GetMontoPendientePorCobrar { get { return _gListaCtasPend.MontoPendientePorCobrar; } }
        public BindingSource CtasPendGetSource { get { return _gListaCtasPend.CtasPendGetSource; } }     
        

        public Tools() 
        {
            _gListaCtasPend = new ListaCtasPend.Lista();
        }

        ToolsFrm frm;
        public void Inicializa()
        {
            _abandonarIsOk = false;
            _gListaCtasPend.Inicializa();
        }
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new ToolsFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }


        private bool _abandonarIsOk;
        public bool AbandonarIsOk { get { return _abandonarIsOk; } }
        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var xmsg = "Abandonar / Cerrar Ficha ?";
            var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == DialogResult.Yes) 
            {
                _abandonarIsOk = true;
            }
        }

        public void BuscarCtasPendientes()
        {
            var filtroOOb = new OOB.CxC.Tools.CtasPendiente.Filtro()
            {
            };
            var r01 = Sistema.MyData.CxC_Tool_CtasPendiente_GetLista(filtroOOb);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            var lst = new List<ListaCtasPend.data>();
            foreach (var rg in r01.ListaD.OrderBy(o=>o.nombreRazonSocial).ToList()) 
            {
                var nr = new ListaCtasPend.data()
                {
                    ciRif = rg.ciRif,
                    cntDocPend = rg.cntDocPend,
                    cntFactPend = rg.cntFactPend,
                    idCliente = rg.idCliente,
                    limiteFactPend = rg.limiteFactPend,
                    montoAcumulado = rg.acumulado,
                    montoImporte = rg.importe,
                    montoLimiteCredito = rg.limiteMontoCredito,
                    nombreRazonSocial = rg.nombreRazonSocial,
                };
                lst.Add(nr);
            }
            _gListaCtasPend.setListaCtasPend(lst);
        }

    }

}