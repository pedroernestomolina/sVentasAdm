using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarNotaAdm
{

    public partial class AgregarNotaAdmFrm : Form
    {

        private IAgregar _controlador;


        public AgregarNotaAdmFrm()
        {
            InitializeComponent();
            InicializaCombo();
        }

        private void InicializaCombo()
        {
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
            L_TITULO.Text = _controlador.TipoNotaAdmGet;
            CB_VENDEDOR.DataSource = _controlador.VendGetSource;
            CB_VENDEDOR.SelectedValue = _controlador.VendGetId;
            L_DOCUMENTO_NRO_CONSECUTIVO.Text=_controlador.NumeroConsecutivoDocGet;
            L_FECHA_EMISION.Text = _controlador.FechaEmisionDocGet.ToShortDateString();
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
        private void CTRL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
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
            _modoInicializar = false;
        }

    }

}