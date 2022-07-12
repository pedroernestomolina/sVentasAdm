using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarCta
{

    public partial class AgregarCtaFrm : Form
    {

        private IAgregar _controlador;


        public AgregarCtaFrm()
        {
            InitializeComponent();
            InicializaCombo();
        }

        private void InicializaCombo()
        {
            CB_TIPO_DOC.DisplayMember = "desc";
            CB_TIPO_DOC.ValueMember = "id";
            //
            CB_VENDEDOR.DisplayMember = "desc";
            CB_VENDEDOR.ValueMember = "id";
        }

        public void setControlador(IAgregar ctr)
        {
            _controlador = ctr;
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
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
        private void AgregarCtaFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK) 
            {
                e.Cancel = false;
            }
        }

        private bool _modoInicializar;
        private void AgregarCtaFrm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            CB_VENDEDOR.DataSource = _controlador.VendGetSource;
            CB_VENDEDOR.SelectedValue = _controlador.VendGetId;
            CB_TIPO_DOC.DataSource = _controlador.TipoDocGetSource;
            CB_TIPO_DOC.SelectedValue = _controlador.TipoDocGetId;
            L_FECHA_VEND_DOC.Text = _controlador.FechaVencDocGet.ToShortDateString();
            TB_SERIE_DOC.Text = _controlador.SerieGet;
            TB_NUM_DOC.Text = _controlador.NumeroDocGet;
            DTP_FECHA_EMISION_DOC.Value = _controlador.FechaEmisionDocGet;
            TB_DIAS_CRED_DOC.Text = _controlador.DiasCreditoDocGet.ToString();
            TB_MONTO_DOC.Text = _controlador.MontoDocGet.ToString();
            TB_FACTOR_DOC.Text = _controlador.TasaFactorDocGet.ToString();
            TB_NOTAS_DOC.Text = _controlador.NotasDocGet;
            ActualizarFichaCliente();
            _modoInicializar = false;
        }

        private void CB_VENDEDOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.setVend("");
            if (CB_VENDEDOR.SelectedIndex != -1) 
            {
                _controlador.setVend(CB_VENDEDOR.SelectedValue.ToString());
            }
        }
        private void CB_TIPO_DOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.setTipoDoc("");
            if (CB_TIPO_DOC.SelectedIndex != -1)
            {
                _controlador.setTipoDoc(CB_TIPO_DOC.SelectedValue.ToString());
            }
        }
        private void CTRL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void TB_SERIE_DOC_Leave(object sender, EventArgs e)
        {
            _controlador.setSerieDoc(TB_SERIE_DOC.Text.Trim());
        }
        private void TB_NUM_DOC_Leave(object sender, EventArgs e)
        {
            _controlador.setNumDoc(TB_NUM_DOC.Text.Trim());
        }
        private void DTP_FECHA_EMISION_DOC_Leave(object sender, EventArgs e)
        {
            _controlador.setFechaEmisionDoc(DTP_FECHA_EMISION_DOC.Value);
            L_FECHA_VEND_DOC.Text = _controlador.FechaVencDocGet.ToShortDateString();
        }
        private void TB_DIAS_CRED_DOC_Leave(object sender, EventArgs e)
        {
            var cnt= int.Parse(TB_DIAS_CRED_DOC.Text);
            _controlador.setDiasCreditoDoc(cnt);
            L_FECHA_VEND_DOC.Text = _controlador.FechaVencDocGet.ToShortDateString();
        }
        private void TB_MONTO_DOC_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_MONTO_DOC.Text);
            _controlador.setMontoDoc(monto);
        }
        private void TB_FACTOR_DOC_Leave(object sender, EventArgs e)
        {
            var tasa = decimal.Parse(TB_FACTOR_DOC.Text);
            _controlador.setFactor(tasa);
        }
        private void TB_NOTAS_DOC_Leave(object sender, EventArgs e)
        {
            _controlador.setNotas(TB_NOTAS_DOC.Text);
        }

        private void TB_CADENA_Leave(object sender, EventArgs e)
        {
        }
        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            _controlador.BuscarCliente();
            if (_controlador.ClienteSeleccionadoIsOk) 
            {
                ActualizarFichaCliente();
            }
        }

        private void ActualizarFichaCliente()
        {
            _modoInicializar = true;
            L_CLIENTE.Text = _controlador.ClienteDataGet;
            CB_VENDEDOR.SelectedValue = _controlador.VendGetId;
            TB_DIAS_CRED_DOC.Text = _controlador.DiasCreditoDocGet.ToString();
            L_FECHA_VEND_DOC.Text = _controlador.FechaVencDocGet.ToShortDateString();
            _modoInicializar = false;
        }

    }

}