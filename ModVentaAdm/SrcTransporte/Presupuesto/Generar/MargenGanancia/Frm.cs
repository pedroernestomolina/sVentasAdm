using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.MargenGanancia
{
    public partial class Frm : Form
    {
        private IBeneficio _controlador;
        private CultureInfo _cult;


        public Frm()
        {
            _cult=CultureInfo.CurrentCulture;
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            L_MONTO_DOC.Text = _controlador.Data.MontoDoc_Get.ToString("n2", _cult);
            TB_ISLR.Text = _controlador.Data.ISLR_Get.ToString("n2", _cult);
            TB_ANTICIPO_ISLR.Text = _controlador.Data.AnticipoISLR_Get.ToString("n2", _cult);
            TB_IGTF_BS.Text = _controlador.Data.IGTFbs_Get.ToString("n2", _cult);
            TB_IGTF_DIVISA.Text = _controlador.Data.IGTFdivisa_Get.ToString("n2", _cult);
            TB_IMP_MUNICIPAL.Text = _controlador.Data.IMP_MUNICIPAL_Get.ToString("n2", _cult);
            CB_BS.Checked = _controlador.Data.IGTF_BS_ACTIVO_Get;
            CB_DIVISA.Checked = _controlador.Data.IGTF_DIVISA_ACTIVO_Get;
            TB_IGTF_BS.Enabled = _controlador.Data.IGTF_BS_ACTIVO_Get;
            TB_IGTF_DIVISA.Enabled = _controlador.Data.IGTF_DIVISA_ACTIVO_Get;
            L_SUBTOTAL.Text = _controlador.Data.SubTotal_Get.ToString("n2", _cult);
            L_ALIADO_PAGO.Text = _controlador.Data.PagoAliado_Get.ToString("n2", _cult);
            L_BENEFICIO.Text = _controlador.Data.MargenBeneficio_Get.ToString("n2", _cult); 
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK)
            {
                e.Cancel = false;
            }
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        public void setControlador(IBeneficio ctr)
        {
            _controlador = ctr;
        }


        private void TB_ISLR_Leave(object sender, EventArgs e)
        {
            var v = Decimal.Parse(TB_ISLR.Text);
            _controlador.Data.setISLR(v);
            TB_ISLR.Text = _controlador.Data.ISLR_Get.ToString("n2", _cult);
            Importe();
        }
        private void TB_ANTICIPO_ISLR_Leave(object sender, EventArgs e)
        {
            var v = Decimal.Parse(TB_ANTICIPO_ISLR.Text);
            _controlador.Data.setAnticipoISLR(v);
            TB_ANTICIPO_ISLR.Text = _controlador.Data.AnticipoISLR_Get.ToString("n2", _cult);
            Importe();
        }
        private void TB_IGTF_BS_Leave(object sender, EventArgs e)
        {
            var v = Decimal.Parse(TB_IGTF_BS.Text);
            _controlador.Data.setIGTFbs(v);
            TB_IGTF_BS.Text = _controlador.Data.IGTFbs_Get.ToString("n2", _cult);
            Importe();
        }
        private void TB_IGTF_DIVISA_Leave(object sender, EventArgs e)
        {
            var v = Decimal.Parse(TB_IGTF_DIVISA.Text);
            _controlador.Data.setIGTFdivisa(v);
            TB_IGTF_DIVISA.Text = _controlador.Data.IGTFdivisa_Get.ToString("n2", _cult);
            Importe();
        }
        private void TB_IMP_MUNICIPAL_Leave(object sender, EventArgs e)
        {
            var v = Decimal.Parse(TB_IMP_MUNICIPAL.Text);
            _controlador.Data.setImpMunicipal(v);
            TB_IMP_MUNICIPAL.Text = _controlador.Data.IMP_MUNICIPAL_Get.ToString("n2", _cult);
            Importe();
        }

        private void TB_ISLR_Validating(object sender, CancelEventArgs e)
        {
            var _tasa = decimal.Parse(TB_ISLR.Text);
            if (_tasa >= 100)
            {
                e.Cancel = true;
            }
        }
        private void TB_ANTICIPO_ISLR_Validating(object sender, CancelEventArgs e)
        {
            var _tasa = decimal.Parse(TB_ANTICIPO_ISLR.Text);
            if (_tasa >= 100)
            {
                e.Cancel = true;
            }
        }
        private void TB_IGTF_BS_Validating(object sender, CancelEventArgs e)
        {
            var _tasa = decimal.Parse(TB_IGTF_BS.Text);
            if (_tasa >= 100)
            {
                e.Cancel = true;
            }
        }
        private void TB_IGTF_DIVISA_Validating(object sender, CancelEventArgs e)
        {
            var _tasa = decimal.Parse(TB_IGTF_DIVISA.Text);
            if (_tasa >= 100)
            {
                e.Cancel = true;
            }
        }
        private void TB_IMP_MUNICIPAL_Validating(object sender, CancelEventArgs e)
        {
            var _tasa = decimal.Parse(TB_IMP_MUNICIPAL.Text);
            if (_tasa >= 100)
            {
                e.Cancel = true;
            }
        }


        private void CB_BS_Leave(object sender, EventArgs e)
        {
            _controlador.Data.setActivarIGTFbs(CB_BS.Checked);
            TB_IGTF_BS.Enabled = CB_BS.Checked;
            Importe();
        }
        private void CB_DIVISA_Leave(object sender, EventArgs e)
        {
            _controlador.Data.setActivarIGTFDivisa(CB_DIVISA.Checked);
            TB_IGTF_DIVISA.Enabled = CB_DIVISA.Checked;
            Importe();
        }
        private void CB_BS_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.Data.setActivarIGTFbs(CB_BS.Checked);
            TB_IGTF_BS.Enabled = CB_BS.Checked;
            Importe();
        }
        private void CB_DIVISA_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.Data.setActivarIGTFDivisa(CB_DIVISA.Checked);
            TB_IGTF_DIVISA.Enabled = CB_DIVISA.Checked;
            Importe();
        }


        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void Importe()
        {
            L_SUBTOTAL.Text = _controlador.Data.SubTotal_Get.ToString("n2", _cult);
            L_ALIADO_PAGO.Text = _controlador.Data.PagoAliado_Get.ToString("n2", _cult);
            L_BENEFICIO.Text = _controlador.Data.MargenBeneficio_Get.ToString("n2", _cult);
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK) 
            {
                Salir();
            }
        }
        private void Salir()
        {
            this.Close();
        }
    }
}