using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Vista
{
    public partial class Frm : Form
    {
        private IVista _controlador;
        //
        public Frm()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            TB_CADENA.Text = _controlador.Doc.Get_CadenaBusq;
            actualizarDatosDoc();
            actualizarData();
            actualizarTotales();
            PN_BUSCAR_CLIENTE.Enabled = true;
            irFocoPrincipal();
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.BtAbandonar.OpcionIsOK || _controlador.ProcesarDocIsOk) 
            {
                e.Cancel = false;
            }
        }
        private void Ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        public void setControlador(Vista.IVista ctr)
        {
            _controlador = ctr;
        }

        private void TB_CADENA_Leave(object sender, EventArgs e)
        {
            _controlador.Doc.setCadenaBuscar(TB_CADENA.Text.Trim().ToUpper());
        }
        private void TB_MOTIVO_Leave(object sender, EventArgs e)
        {
            _controlador.Doc.DocGenerar.setMotivo(TB_MOTIVO.Text.Trim());
        }
        private void TB_EXENTO_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_EXENTO.Text);
            _controlador.Doc.DocGenerar.MontoExento.setBase(monto);
            actualizarTotales();
        }
        private void TB_BASE_1_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_BASE_1.Text);
            _controlador.Doc.DocGenerar.MontoFiscal_1.setBase(monto);
            actualizarTotales();
        }
        private void TB_BASE_2_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_BASE_2.Text);
            _controlador.Doc.DocGenerar.MontoFiscal_2.setBase(monto);
            actualizarTotales();
        }
        private void TB_BASE_3_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_BASE_3.Text);
            _controlador.Doc.DocGenerar.MontoFiscal_3.setBase(monto);
            actualizarTotales();
        }

        private void BT_LIMPIAR_Click(object sender, EventArgs e)
        {
            LimpiarDoc();
            actualizarData();
            actualizarTotales();
        }
        private void BT_PROCESAR_DOC_Click(object sender, EventArgs e)
        {
            ProcesarDoc();
        }
        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            BuscarDocumentos();
        }
        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        //
        private void ProcesarDoc()
        {
            _controlador.ProcesarDoc();
            if (_controlador.ProcesarDocIsOk) 
            {
                salir();
            }
        }
        private void BuscarDocumentos()
        {
            _controlador.Doc.BuscarDocumentos();
            TB_CADENA.Text = _controlador.Doc.Get_CadenaBusq;
            actualizarData();
            actualizarTotales();
            actualizarDatosDoc();
            if (_controlador.Doc.BusquedaIsOk)
            {
                PN_BUSCAR_CLIENTE.Enabled = false;
            }
        }
        private void LimpiarDoc()
        {
            _controlador.LimpiarDoc();
            PN_BUSCAR_CLIENTE.Enabled = true;
            actualizarDatosDoc();
            irFocoPrincipal();
        }
        private void actualizarDatosDoc()
        {
            L_CLIENTE.Text = _controlador.Doc.Get_DocAplicarNotaCredito_DatosCliente;
            L_DOCUMENTO.Text = _controlador.Doc.Get_DocAplicarNotaCredito_DatosDocumento; ;
        }
        void actualizarData() 
        {
            TB_MOTIVO.Text = _controlador.Doc.DocGenerar.Get_Motivo;
            TB_EXENTO.Text = _controlador.Doc.DocGenerar.MontoExento.Get_Base.ToString("n2").Replace(".", "");
            TB_BASE_1.Text = _controlador.Doc.DocGenerar.MontoFiscal_1.Get_Base.ToString("n2").Replace(".", "");
            TB_BASE_2.Text = _controlador.Doc.DocGenerar.MontoFiscal_2.Get_Base.ToString("n2").Replace(".", "");
            TB_BASE_3.Text = _controlador.Doc.DocGenerar.MontoFiscal_3.Get_Base.ToString("n2").Replace(".", "");
        }
        private void actualizarTotales()
        {
            L_TASA_1.Text = _controlador.Doc.DocGenerar.MontoFiscal_1.Get_Tasa.ToString("n2")+"%";
            L_TASA_2.Text = _controlador.Doc.DocGenerar.MontoFiscal_2.Get_Tasa.ToString("n2") + "%";
            L_TASA_3.Text = _controlador.Doc.DocGenerar.MontoFiscal_3.Get_Tasa.ToString("n2") + "%";
            L_IVA_1.Text = _controlador.Doc.DocGenerar.MontoFiscal_1.Get_Iva.ToString("n2");
            L_IVA_2.Text = _controlador.Doc.DocGenerar.MontoFiscal_2.Get_Iva.ToString("n2");
            L_IVA_3.Text = _controlador.Doc.DocGenerar.MontoFiscal_3.Get_Iva.ToString("n2");
            L_SUBT_BASE.Text = _controlador.Doc.DocGenerar.Get_Subt_Base.ToString("n2");
            L_SUBT_IMP.Text = _controlador.Doc.DocGenerar.Get_Subt_Imp.ToString("n2");
            L_TOTAL.Text = _controlador.Doc.DocGenerar.Get_Total.ToString("n2");
        }
        private void AbandonarFicha()
        {
            _controlador.BtAbandonar.Opcion();
            if (_controlador.BtAbandonar.OpcionIsOK) 
            {
                salir();
            }
        }
        private void salir()
        {
            this.Close();
        }
        private void irFocoPrincipal()
        {
            tabControl1.TabPages[0].Focus();
            TB_CADENA.Focus();
        }
    }
}